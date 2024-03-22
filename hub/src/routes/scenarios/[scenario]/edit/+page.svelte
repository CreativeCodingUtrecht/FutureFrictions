<script lang="ts">
	import { Stepper, Step } from '@skeletonlabs/skeleton';
	import { TabGroup, Tab, TabAnchor } from '@skeletonlabs/skeleton';
	import { page } from '$app/stores';

	import type { PageData } from './$types';
	import { FF_WEBGL_URL } from '$lib/constants';

	export let data: PageData;

	const scenario = data.scenario;
	let json = data.json;

	const images = data.images;

	const title = json.friction.description;
	const welcome = json.scene.content.welcome;
	const background = json.scene.background;
	const avatar = json.scene.avatar;

	const background_url = background
		? `/api/scenarios/${scenario}/${background}`
		: `/placeholders/background.jpg`;
	const avatar_url = avatar ? `/api/scenarios/${scenario}/${avatar}` : `/placeholders/avatar.jpg`;

	const url = `${FF_WEBGL_URL}?scenario=${scenario}`;

	let raw = JSON.stringify(data.json, null, 4);

	const image_url = `/api/scenarios/${scenario}/`;

	const confirmRemove = (e) => {
		if (!confirm('Are you sure you want to remove this image? 🤔')) {
			e.preventDefault();
		}
	};

	const save = (e) => {
		document.getElementById("save").submit();
	}
</script>

<div>
	<h2 class="h2">Scenario definition</h2>
	<br />
	<form method="POST" id="save" class="space-y-4" action="?/save">
		<label class="label space-y-4">
			<span>Title</span>

			<input
				bind:value={json.friction.description}
				class="input"
				title="Title"
				type="text"
				name="title"
			/>
		</label>

		<input type="hidden" name="json" value={JSON.stringify(json)} />
		<p>
			<button class="btn variant-filled-primary">Save</button>
		</p>
	</form>
	<br />

	<Stepper buttonNext="btn variant-ghost-primary" on:complete={save}>
		<Step>
			<svelte:fragment slot="header">Intent</svelte:fragment>
			<div class="space-y-4">
				<label class="label space-y-4">
					<span
						>Fill out this text box with a frictional statement you would like to explore further</span
					>
					<textarea
						bind:value={json.scene.content.welcome}
						class="textarea"
						title="Frictional statement"
						rows="6"
						name="statement"
					/>
				</label>

				<label class="label space-y-4">
					<span>Narrator's avatar</span>

					<input
						bind:value={json.scene.avatar}
						class="input"
						title="Avatar"
						type="text"
						name="scene_avatar"
					/>
				</label>
			</div>
		</Step>
		<Step>
			<svelte:fragment slot="header">Present</svelte:fragment>
			<div class="space-y-4">
				<label class="label space-y-4">
					<span>Collage of the present</span>
					<input
						bind:value={json.scene.background}
						class="input"
						title="Background"
						type="text"
						name="scene_background"
					/>
				</label>

				<!-- Actor 1 -->
				<div class="card">
					<header class="card-header">Actor one</header>
					<section class="p-4 space-y-4">
						<label class="label space-y-4">
							<span>Name of the actor</span>
							<input
								bind:value={json.actors[0].description}
								class="input"
								title="Name of the actor"
								type="text"
								name="actor1name"
							/>
						</label>

						<label class="label">
							<span>Story of the present</span>
							<textarea
								bind:value={json.actors[0].content.before}
								class="textarea"
								title="Actor statement"
								rows="3"
								name="statement"
							/>
						</label>

						<label class="label">
							<span>Avatar</span>
							<input
								bind:value={json.actors[0].avatar}
								class="input"
								title="Name of the actor"
								type="text"
								name="actor1name"
							/>
						</label>

						<label class="label">
							<span>Sprite</span>
							<input
								bind:value={json.actors[0].sprite}
								class="input"
								title="Name of the actor"
								type="text"
								name="actor1name"
							/>
						</label>
					</section>
				</div>

				<!-- Actor 2 -->
				<div class="card">
					<header class="card-header">Actor two</header>
					<section class="p-4 space-y-4">
						<label class="label space-y-4">
							<span>Name of the actor</span>
							<input
								bind:value={json.actors[1].description}
								class="input"
								title="Name of the actor"
								type="text"
								name="actor1name"
							/>
						</label>

						<label class="label">
							<span>Story of the present</span>
							<textarea
								bind:value={json.actors[1].content.before}
								class="textarea"
								title="Actor statement"
								rows="3"
								name="statement"
							/>
						</label>

						<label class="label">
							<span>Avatar</span>
							<input
								bind:value={json.actors[1].avatar}
								class="input"
								title="Name of the actor"
								type="text"
								name="actor1name"
							/>
						</label>

						<label class="label">
							<span>Sprite</span>
							<input
								bind:value={json.actors[1].sprite}
								class="input"
								title="Name of the actor"
								type="text"
								name="actor1name"
							/>
						</label>
					</section>
				</div>
				<!-- Actor 3 -->
				<div class="card">
					<header class="card-header">Actor three</header>
					<section class="p-4 space-y-4">
						<label class="label space-y-4">
							<span>Name of the actor</span>
							<input
								bind:value={json.actors[2].description}
								class="input"
								title="Name of the actor"
								type="text"
								name="actor1name"
							/>
						</label>

						<label class="label">
							<span>Story of the present</span>
							<textarea
								bind:value={json.actors[2].content.before}
								class="textarea"
								title="Actor statement"
								rows="3"
								name="statement"
							/>
						</label>

						<label class="label">
							<span>Avatar</span>
							<input
								bind:value={json.actors[2].avatar}
								class="input"
								title="Name of the actor"
								type="text"
								name="actor1name"
							/>
						</label>

						<label class="label">
							<span>Sprite</span>
							<input
								bind:value={json.actors[2].sprite}
								class="input"
								title="Name of the actor"
								type="text"
								name="actor1name"
							/>
						</label>
					</section>
				</div>
			</div>
		</Step>
		<Step>
			<svelte:fragment slot="header">What If..</svelte:fragment>
			<div class="space-y-4">
				<span
					>Think of a near future (10 years for now) and brainstorm about a provocative WHAT IF
					question-related to the friction, its context, and the relationship with the characters
					you defined. (for example: what if local municipalities allocate robots in each
					neighborhood to help reduce street litter?)</span
				>

				<textarea
					bind:value={json.friction.content.before}
					class="textarea"
					title="HTML content van de cel"
					rows="6"
					name="whatif"
				/>

				<label class="label">
					<span>Intervention's avatar</span>
					<input
						bind:value={json.friction.avatar}
						class="input"
						title="Background"
						type="text"
						name="scene_background"
					/>
				</label>
			</div>
		</Step>
		<Step>
			<svelte:fragment slot="header">Future</svelte:fragment>

			<label class="label space-y-4">
				<span>Collage of the future</span>
				<input
					bind:value={json.friction.options.a.alternativeBackground}
					class="input"
					title="Background"
					type="text"
					name="scene_background"
				/>
			</label>

			<!-- 
                @TODO Add sprites foreground, background, floating
            -->

			<!-- Actor 1 -->
			<div class="card">
				<header class="card-header">Actor one</header>
				<section class="p-4 space-y-4">
					<label class="label space-y-4">
						<span>Name of the actor</span>
						<input
							bind:value={json.actors[0].description}
							class="input"
							title="Name of the actor"
							type="text"
							name="actor1name"
							readonly
						/>
					</label>

					<label class="label">
						<span>Story of the future</span>
						<textarea
							bind:value={json.actors[0].content.after.a}
							class="textarea"
							title="Actor statement"
							rows="3"
							name="statement"
						/>
					</label>
				</section>
			</div>

			<!-- Actor 2 -->
			<div class="card">
				<header class="card-header">Actor two</header>
				<section class="p-4 space-y-4">
					<label class="label space-y-4">
						<span>Name of the actor</span>
						<input
							bind:value={json.actors[1].description}
							class="input"
							title="Name of the actor"
							type="text"
							name="actor1name"
							readonly
						/>
					</label>

					<label class="label">
						<span>Story of the future</span>
						<textarea
							bind:value={json.actors[1].content.after.a}
							class="textarea"
							title="Actor statement"
							rows="3"
							name="statement"
						/>
					</label>
				</section>
			</div>
			<!-- Actor 3 -->
			<div class="card">
				<header class="card-header">Actor three</header>
				<section class="p-4 space-y-4">
					<label class="label space-y-4">
						<span>Name of the actor</span>
						<input
							bind:value={json.actors[2].description}
							class="input"
							title="Name of the actor"
							type="text"
							name="actor1name"
							readonly
						/>
					</label>

					<label class="label">
						<span>Story of the future</span>
						<textarea
							bind:value={json.actors[2].content.after.a}
							class="textarea"
							title="Actor statement"
							rows="3"
							name="statement"
						/>
					</label>
				</section>
			</div>
		</Step>
	</Stepper>
	<br />
	<form method="POST" action="?/save">
		<input type="hidden" name="json" value={JSON.stringify(json)} />
		<p>
			<button class="btn variant-filled-primary">Save</button>
		</p>
	</form>
</div>
