<script lang="ts">
	import { fabric } from 'fabric';
	import { onMount } from 'svelte';
	import { Accordion, AccordionItem, SlideToggle } from '@skeletonlabs/skeleton';
	import {v4 as uuidv4} from 'uuid';

	export let scenario: String;
	export let backgrounds: String[] = [];
	export let elements: String[] = [];
	export let collage: any;
	export let definition: any = {};
	export let file: File | undefined = undefined;
	export let includeFriction: Boolean = false;

	const FABRIC_CONTROL_VISIBILITY = { mtr: true, mb: false, mt: false, ml: false, mr: false };
	const FABRIC_SCALE_NEW_OBJECT = 0.5;
	const FABRIC_SCALE_LIMIT_PX = 32;
	const FABRIC_BACKGROUND_COLOR = '#ddddee';
	const FABRIC_INCLUDE_IN_EXPORT = [
		'meta',
		'_controlsVisibility',
		'minScaleLimit',
		'lockScalingFlip'
	];
	let canvasSectionHeight = 1080;

	let canvas: fabric.Canvas | undefined;

	let selectedObject: fabric.Object | undefined;

	const serializeCanvasHandler = () => {
		collage = canvas?.toObject(FABRIC_INCLUDE_IN_EXPORT);
		collage = cleanCollageUrls(collage);
		updateCanvasDefinition();
		updateExportFile();
	};

	const cleanCollageUrls = (collage) => {
		if (collage.backgroundImage && collage.backgroundImage.src) {
			collage.backgroundImage.src = new URL(
				collage.backgroundImage.src,
				'http://localhost'
			).pathname;
		}

		if (collage.objects) {
			collage.objects.forEach((obj) => {
				if (obj.src) {
					obj.src = new URL(obj.src, 'http://localhost').pathname;
				}
			});
		}

		return collage;
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

		let background;
		let backgroundColor;
		const elements = [];

		// Fetch background
		if (collage.backgroundImage) {
			background = collage.backgroundImage.src;
		}

		if (collage.background) {
			backgroundColor = collage.background;
		}

		// Fetch images in collage relevant for visualization
		let idxObject = 0;
		let idxElement = 0;

		if (collage.objects) {
			collage.objects.forEach((obj) => {
				// Fetch image info
				const image = {
					url: obj.src,
					placement: {
						index: idxObject,
						left: obj.left,
						top: obj.top,
						width: obj.width,
						height: obj.height,
						scaleX: obj.scaleX,
						scaleY: obj.scaleY,
						angle: obj.angle
					},
					...(obj.meta?.friction == true ) ? { friction: true } : {friction: false},
					...(obj.meta?.interactable == true ) ? { interactable: true,
						...(obj.meta?.interactable == true ) && { interaction: {
							name: obj.meta?.name,
							statement: obj.meta?.statement
						}}
					} : {interactable: false},
				};

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
			elements
		};
	};

	onMount(() => {
		const wrapper: HTMLElement | null = document.getElementById('canvas-wrapper');
		const canvasElem: HTMLElement | null = document.getElementById('collage-canvas');

		// console.log('Collage component:', scenario, backgrounds, elements);

		canvas = new fabric.Canvas('collage-canvas', {
			width: 1920,
			height: 1080,
			backgroundColor: FABRIC_BACKGROUND_COLOR,
			hoverCursor: 'pointer',
			preserveObjectStacking: true
		});

		if (collage) {
			canvas.loadFromJSON(collage, () => {
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

		const handleObjectSelected = (obj) => {
			selectedObject = canvas.getActiveObject();
			console.log('selectedObject', selectedObject);
		};

		const handleObjectDeselected = () => {
			selectedObject = undefined;
			console.log('selectedObject', selectedObject);
		};

		wrapper?.addEventListener('keyup', handleKeyUp);
		window.addEventListener('resize', handleResize);
		canvas.on('object:modified', serializeCanvasHandler);
		canvas.on('object:selected', handleObjectSelected);
		canvas.on('selection:updated', handleObjectSelected);
		canvas.on('selection:created', handleObjectSelected);
		canvas.on('selection:cleared', handleObjectDeselected);

		handleResize();
	});

	const updatedSelectedObjectFriction = (e) => {
		const checked = e.srcElement.checked;
		var obj = canvas.getActiveObject();

		if (obj) {
			if (!obj.meta) {
				obj.meta = {};
			}

			obj.meta['friction'] = checked;
		}

		serializeCanvasHandler();
	};

	const updatedSelectedObjectInteractable = (e) => {
		const checked = e.srcElement.checked;
		var obj = canvas.getActiveObject();

		if (obj) {
			if (!obj.meta) {
				obj.meta = {};
			}

			obj.meta['interactable'] = checked;
		}

		serializeCanvasHandler();
	};

	const updatedSelectedObjectName = (e) => {
		var obj = canvas.getActiveObject();

		if (obj) {
			if (!obj.meta) {
				obj.meta = {};
			}

			obj.meta['name'] = e.target.value;
		}

		serializeCanvasHandler();
	};

	const updatedSelectedObjectStatement = (e) => {
		var obj = canvas.getActiveObject();

		if (obj) {
			if (!obj.meta) {
				obj.meta = {};
			}

			obj.meta['statement'] = e.target.value;
		}

		serializeCanvasHandler();
	};

	const updateSelectedObjectBringToFront = () => {
		var obj = canvas.getActiveObject();

		if (obj) {
			canvas?.bringToFront(obj);
			canvas?.renderAll();
		}

		serializeCanvasHandler();
	}

	const updateSelectedObjectBringForward = () => {
		var obj = canvas.getActiveObject();

		if (obj) {
			canvas?.bringForward(obj, true);
			canvas?.renderAll();
		}

		serializeCanvasHandler();
	}		

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

	const addElement = (element) => {
		const url = `/api/scenarios/${scenario}/element/${element}`;
		fabric.Image.fromURL(
			url,
			(img) => {
				img.meta = {
					image: element,
					role: 'element',
					uuid: uuidv4()
				};

				// scale image down, and flip it, before adding it onto canvas
				img.scale(FABRIC_SCALE_NEW_OBJECT);
				img.minScaleLimit = FABRIC_SCALE_LIMIT_PX / canvas?.getZoom() / img.getOriginalSize().width;
				img.lockScalingFlip = true;
				img.setControlsVisibility(FABRIC_CONTROL_VISIBILITY);
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
	<section
		class="grid grid-rows-1 md:grid-cols-4 grid-cols-1 gap-2 h-[{canvasSectionHeight}px]"
		id="collage-wrapper"
	>
		<div class="card variant-ghost-tertiary">
			<section class="p-4 overflow-auto max-h-[500px]">
				<Accordion class="h-fit">
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
									</div>
								{/each}
							</section>
						</svelte:fragment>
					</AccordionItem>
					<AccordionItem open>
						<svelte:fragment slot="lead">🌳</svelte:fragment>
						<svelte:fragment slot="summary">Elements</svelte:fragment>
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
				<canvas width="1920" height="1080" id="collage-canvas"></canvas>
			</section>
		</div>
		{#if selectedObject}
			<div class="md:col-span-1"></div>
			<div class="card md:col-span-3 variant-ghost-tertiary" id="object-controls" tabindex="2">
				
				<section class="grid grid-cols-6 gap-2">					
					<div class="col-span-1 p-10">
						<img src={selectedObject._element?.src} />
					</div>					
					<div class="col-span-5 p-4 space-y-4">
						<h6 class="h6">Turn this object into a character!</h6>
						{#if includeFriction}
						<label class="flex items-center space-x-2">
							<SlideToggle name="slider-label" 
							 	size="sm"
								bind:checked={selectedObject.meta['friction']}
								on:change={updatedSelectedObjectFriction}>
								Does this object represent the intervention / catalyst of these future frictions?</SlideToggle>
						</label>
						{/if}
						<label class="flex items-center space-x-2">
							<SlideToggle name="slider-label" size="sm"
								bind:checked={selectedObject.meta['interactable']}
								on:change={updatedSelectedObjectInteractable}
								>Is this an interviewable human/non-human character?</SlideToggle>
						</label>
						{#if selectedObject.meta['interactable']}						
							<label class="label space-y-4">
								<span>What's the name of this character?</span>
								<input								
									bind:value={selectedObject.meta['name']}
									on:change={updatedSelectedObjectName}
									class="input"
									title="Name"
									type="text"
								/>
							</label>

							<label class="label space-y-4">
								<span>What do you think this character might say about the situation?</span>
								<textarea
									bind:value={selectedObject.meta['statement']}
									on:change={updatedSelectedObjectStatement}
									class="textarea"
									title="Statement"
									rows="3"
								/>
							</label>					
						{/if}
						<h6 class="h6">Use the following buttons the bring the object forward in the collage</h6>
						<a class="btn variant-ghost-primary" on:click={updateSelectedObjectBringToFront}>Bring to front</a>
						<a class="btn variant-ghost-primary" on:click={updateSelectedObjectBringForward}>Bring foreward</a>
					</div>
				</section>
			</div>
		{/if}		
	</section>
</div>
