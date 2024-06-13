<script lang="ts">
	import { fabric } from 'fabric';
	import { onMount } from 'svelte';
	import { Accordion, AccordionItem } from '@skeletonlabs/skeleton';

	export let scenario: String;
	export let backgrounds: String[] = [];
	export let elements: String[] = [];
	export let characters: String[] = [];
	export let collage: any;
	export let definition: any = {};
	export let file: File | undefined = undefined;

	const FABRIC_CONTROL_VISIBILITY = { mtr: false, mb: false, mt: false, ml: false, mr: false };
	const FABRIC_SCALE_NEW_OBJECT = 0.5;
	const FABRIC_BACKGROUND_COLOR = '#ddddee';
	let canvasSectionHeight = 1080;

	let canvas: fabric.Canvas | undefined;

	// console.log("Collage",collage);
	// console.log("Definition",definition);

	const serializeCanvasHandler = () => {
		collage = canvas?.toObject(['meta']);
		// console.log("Serializing canvas", collage);
		updateCanvasDefinition();
		updateExportFile();
	};

	const updateExportFile = () => {
			// Save canvas to blob (base64 encoded image)
			const dataURI = canvas?.toDataURL({
				multiplier: 1920 / canvas.getWidth()
			});
			const binary = atob(dataURI.split(',')[1]);
			const array = [];
			for (var i = 0; i < binary.length; i++) {
				array.push(binary.charCodeAt(i));
			}
			const blob = new Blob([new Uint8Array(array)], { type: 'octet/stream' });
			file = new File([blob], 'collage.png', { type: 'image/png' });
			console.log('Collage export file', file);
		};

		const updateCanvasDefinition = () => {
			if (!collage) return;

			const prevdefinition = definition || {};

			let background;
			let backgroundColor;
			const characters = [];
			const elements = [];

			// Fetch background
			if (collage.backgroundImage) {
				background = new URL(collage.backgroundImage.src).pathname;
			}

			if (collage.background) {
				backgroundColor = collage.background;
			}

			// Fetch images in collage relevant for visualization
			let idxObject = 0;
			let idxCharacter = 0;
			let idxElement = 0;

			if (collage.objects) {
				collage.objects.forEach((obj) => {
					// Fetch image info
					const image = {
						url: new URL(obj.src).pathname,
						placement: {
							index: idxObject,
							left: obj.left,
							top: obj.top,
							width: obj.width,
							height: obj.height,
							scaleX: obj.scaleX,
							scaleY: obj.scaleY,
							angle: obj.angle
						}
					};

					// Fetch characters
					if (obj.meta?.role === 'character') {
						image.name = prevdefinition.characters[idxCharacter]?.name || '';
						image.statement = prevdefinition.characters[idxCharacter]?.statement || '';
						image.id = idxCharacter;
						characters.push(image);
						idxCharacter++;
					}

					// Fetch elements
					if (obj.meta?.role === 'element') {
						image.id = idxElement;
						elements.push(image);
						idxElement++;
					}

					idxObject++;
				});
			}

			definition = {
				backgroundColor,
				background,
				characters,
				elements
			};

			// console.log("Canvas object definition", definition);
		};	

	onMount(() => {
		const wrapper: HTMLElement | null = document.getElementById('canvas-wrapper');
		const canvasElem: HTMLElement | null = document.getElementById('collage-canvas');

		// console.log('Collage component:', scenario, backgrounds, elements, characters);

		canvas = new fabric.Canvas('collage-canvas', {
			width: 1920,
			height: 1080,
			backgroundColor: FABRIC_BACKGROUND_COLOR
		});

		if (collage) {
			canvas.loadFromJSON(collage, () => {
				// canvas?.renderAll();
				canvas.setBackgroundColor(FABRIC_BACKGROUND_COLOR, () => {
					canvas?.renderAll();
					serializeCanvasHandler();
				});
			});
		}

		const handleResize = () => {
			const width = wrapper.offsetWidth - 30;
			const height = (width / 16) * 9;

			canvas?.setWidth(width);
			canvas?.setHeight(height);

			canvas?.setZoom(width / 1920);
			canvas?.renderAll();

			canvasSectionHeight = height;
		};

		const handleKeyUp = (e) => {
			console.log(e);
			var selectedObjects = canvas?.getActiveObjects() || [];

			if (e.keyCode == 46 || e.key === 'Delete' || e.code === 'Delete' || e.key === 'Backspace') {
				if (selectedObjects.length > 0) {
					selectedObjects?.forEach((obj) => {
						console.log('Deleting selected element(s) on canvas');
						canvas?.remove(obj);
					});
					canvas?.discardActiveObject().renderAll();
					serializeCanvasHandler();
				}
			}
		};

		const handleObjectSelected = (e) => {
			console.log('Object selected', e.target);
		};

		wrapper?.addEventListener('keyup', handleKeyUp);
		window.addEventListener('resize', handleResize);
		canvas.on('object:selected', handleObjectSelected);
		canvas.on('object:modified', serializeCanvasHandler);

		handleResize();
	});

	const addBackground = (background) => {
		const url = `/api/scenarios/${scenario}/background/${background}`;
		fabric.Image.fromURL(
			url,
			function (img) {
				const scaleX = 1920 / img.width;
				const scaleY = 1080 / img.height;
				canvas?.setBackgroundImage(img, canvas.renderAll.bind(canvas), {
					scaleX,
					scaleY
				});
				serializeCanvasHandler();
			},
			{ crossOrigin: 'anonymous' }
		);
	};

	const addCharacter = (character: String) => {
		const url = `/api/scenarios/${scenario}/character/${character}`;

		fabric.Image.fromURL(
			url,
			function (img) {
				// scale image down, and flip it, before adding it onto canvas
				img.meta = {
					image: character,
					role: 'character'
				};

				img.scale(FABRIC_SCALE_NEW_OBJECT);
				img.setControlsVisibility(FABRIC_CONTROL_VISIBILITY);
				canvas?.add(img);
				canvas?.setActiveObject(img);
				canvas?.viewportCenterObject(img);
				canvas?.renderAll();
				serializeCanvasHandler();
			},
			{ crossOrigin: 'anonymous', perPixelTargetFind: true }
		);
	};

	const addElement = (element) => {
		const url = `/api/scenarios/${scenario}/element/${element}`;
		fabric.Image.fromURL(
			url,
			(img) => {
				img.meta = {
					image: element,
					role: 'element'
				};

				// scale image down, and flip it, before adding it onto canvas
				img.scale(FABRIC_SCALE_NEW_OBJECT);
				img.setControlsVisibility({ mtr: false, mb: false, mt: false, ml: false, mr: false });
				canvas?.add(img);
				canvas?.setActiveObject(img);
				canvas?.viewportCenterObject(img);
				canvas?.renderAll();
				serializeCanvasHandler();
			},
			{ crossOrigin: 'anonymous', perPixelTargetFind: true, left: 200, top: 200 }
		);
	};
</script>

<div class="container sm">
	<section class="grid grid-rows-1 md:grid-cols-4 grid-cols-1 gap-2 h-[{canvasSectionHeight}px]" id="collage-wrapper">
		<div class="card variant-ghost-tertiary">
			<section class="p-4 overflow-auto max-h-[500px]">
				<Accordion autocollapse class="h-fit">
					<AccordionItem open>
						<svelte:fragment slot="lead">🚞</svelte:fragment>
						<svelte:fragment slot="summary">Backgrounds</svelte:fragment>
						<svelte:fragment slot="content">
							<section class="grid grid-cols-2 md:grid-cols-3 gap-4">
								{#each backgrounds as background}
									<div>
										<img
											on:click={() => addBackground(background)}
											draggable="false"
											class="h-auto max-w-full rounded-sm"
											alt=""
											src="/api/scenarios/{scenario}/background/{background}"
										/>
										<!-- on:dragstart={dragElement} -->
									</div>
								{/each}
							</section>
						</svelte:fragment>
					</AccordionItem>
					<AccordionItem>
						<svelte:fragment slot="lead">👩‍🦰</svelte:fragment>
						<svelte:fragment slot="summary">Characters</svelte:fragment>
						<svelte:fragment slot="content">
							<section class="grid grid-cols-2 md:grid-cols-3 gap-4">
								{#each characters as character}
									<div>
										<img
											draggable="false"
											on:click={() => addCharacter(character)}
											class="h-auto max-w-full rounded-sm"
											alt=""
											src="/api/scenarios/{scenario}/character/{character}"
										/>
									</div>
								{/each}
							</section>
						</svelte:fragment>
					</AccordionItem>
					<AccordionItem>
						<svelte:fragment slot="lead">🌳</svelte:fragment>
						<svelte:fragment slot="summary">Ambient</svelte:fragment>
						<svelte:fragment slot="content">
							<section class="grid grid-cols-2 md:grid-cols-3 gap-4">
								{#each elements as element}
									<div>
										<img
											draggable="false"
											on:click={() => addElement(element)}
											class="h-auto max-w-full rounded-sm"
											alt=""
											src="/api/scenarios/{scenario}/element/{element}"
										/>
									</div>
								{/each}
							</section>
						</svelte:fragment>
					</AccordionItem>
				</Accordion>
			</section>
		</div>
		<div class="card md:col-span-3 variant-ghost-tertiary" id="canvas-wrapper" tabindex="1">
			<section class="p-4">
				<!-- on:dragover={allowDrop} on:drop={dropElement} -->
				<canvas width="1920" height="1080" id="collage-canvas"></canvas>
			</section>
		</div>
	</section>
</div>
