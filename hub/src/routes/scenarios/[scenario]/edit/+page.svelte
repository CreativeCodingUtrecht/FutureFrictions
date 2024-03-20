<script lang="ts">
	import { Stepper, Step } from '@skeletonlabs/skeleton';

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

    const debuggie = () => {
        console.log(json);
    }
</script>

<h1>{title}</h1>

<div class="container pt-5">
	<ul class="menu px-3 border bg-base-100 menu-horizontal rounded-box">
		<!-- <li class="menu-title">{title}</li> -->
		<li class=""><a href="/scenarios/{scenario}" target="FF_WEBGL_URL">View</a></li>
		<li class=""><a href={url} target="FF_WEBGL_URL">Play</a></li>
		<li class="inline-block font-bold border-b-2 text-base-content border-primary">
			<a href="/scenarios/{scenario}/edit">Edit</a>
		</li>
	</ul>
</div>

<!-- <div class="container pt-5">
    <div class="flex items-center w-full px-4 py-10 bg-cover card bg-base-200" style="background-image: url(&quot;{background_url}&quot;);">
        <div class="card glass lg:card-side text-neutral-content">
          <figure class="p-6">
            <img src="{avatar_url}" class="rounded-lg shadow-lg" style="max-width:200px">
          </figure> 
          <div class="max-w-md card-body">
            <h2 class="card-title">{title}</h2> 
            <p>{welcome}</p> 
            <div class="card-actions">
                <a href="{url}" class="btn btn-primary" target="FF_WEBGL_URL">Play</a>
            </div>
          </div>
        </div>
      </div>
</div> -->

<div>
	<Stepper>
		<Step>
			<svelte:fragment slot="header">Intent</svelte:fragment>

			<label class="label">
				<span>Fill out this text box with a frictional statement you would like to explore further</span>

                <textarea
					bind:value={json.scene.content.welcome}
					class="textarea"
					title="Frictional statement"
					rows="6"
					name="statement"
				/>
			</label>

			<label class="label">
				<span>Narrator's avatar</span>

                <input
                    bind:value={json.scene.avatar}
                    class="input"
                    title="Avatar"
                    type="text"
                    name="scene_avatar"
                />

			</label>
		</Step>
		<Step>
			<svelte:fragment slot="header">Present</svelte:fragment>

			<label class="label">
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
				<header class="card-header">
                    Actor 1
				</header>
				<section class="p-4">
					<label class="label">
						<span>Name of the actor</span>
						<input
							bind:value={json.actors[0].description}
							class="input"
							title="Name of the actor"
							type="text"
							name="actor1name"
						/>

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

						<span>Avatar</span>
						<input
							bind:value={json.actors[0].avatar}
							class="input"
							title="Name of the actor"
							type="text"
							name="actor1name"
						/>
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
				<!-- <footer class="card-footer">(footer)</footer> -->
			</div>

			<!-- Actor 2 -->
			<div class="card">
				<header class="card-header">
                    Actor 2
				</header>
				<section class="p-4">
					<label class="label">
						<span>Name of the actor</span>
						<input
							bind:value={json.actors[1].description}
							class="input"
							title="Name of the actor"
							type="text"
							name="actor1name"
						/>

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

						<span>Avatar</span>
						<input
							bind:value={json.actors[1].avatar}
							class="input"
							title="Name of the actor"
							type="text"
							name="actor1name"
						/>
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
				<!-- <footer class="card-footer">(footer)</footer> -->
			</div>            

			<!-- Actor 3 -->
			<div class="card">
				<header class="card-header">
                    Actor 3
				</header>
				<section class="p-4">
					<label class="label">
						<span>Name of the actor</span>
						<input
							bind:value={json.actors[2].description}
							class="input"
							title="Name of the actor"
							type="text"
							name="actor1name"
						/>

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

						<span>Avatar</span>
						<input
							bind:value={json.actors[2].avatar}
							class="input"
							title="Name of the actor"
							type="text"
							name="actor1name"
						/>
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
				<!-- <footer class="card-footer">(footer)</footer> -->
			</div>            
		</Step>
		<Step>
			<svelte:fragment slot="header">What If..</svelte:fragment>
			<span
				>Think of a near future (10 years for now) and brainstorm about a provocative WHAT IF
				question-related to the friction, its context, and the relationship with the characters you
				defined. (for example: what if local municipalities allocate robots in each neighborhood to
				help reduce street litter?)</span
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
		</Step>
		<Step>
			<svelte:fragment slot="header">Future</svelte:fragment>

            <label class="label">
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
				<header class="card-header">
                    Actor 1
				</header>
				<section class="p-4">
					<label class="label">
						<span>Actor's name</span>
						<input
							bind:value={json.actors[0].description}
							class="input"
							title="Name of the actor"
                            readonly
							type="text"
							name="actor1name"
						/>

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
					</label>
				</section>
				<!-- <footer class="card-footer">(footer)</footer> -->
			</div>

			<!-- Actor 2 -->
			<div class="card">
				<header class="card-header">
                    Actor 2
				</header>
				<section class="p-4">
					<label class="label">
						<span>Actor's name</span>
						<input
							bind:value={json.actors[1].description}
							class="input"
							title="Name of the actor"
                            readonly
							type="text"
							name="actor1name"
						/>

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
					</label>
				</section>
				<!-- <footer class="card-footer">(footer)</footer> -->
			</div>            

			<!-- Actor 3 -->
			<div class="card">
				<header class="card-header">
                    Actor 3
				</header>
				<section class="p-4">
					<label class="label">
						<span>Actor's name</span>
						<input
							bind:value={json.actors[2].description}
							class="input"
							title="Name of the actor"
                            readonly
							type="text"
							name="actor1name"
						/>

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
					</label>
				</section>
				<!-- <footer class="card-footer">(footer)</footer> -->
			</div>            
		</Step>
	</Stepper>
</div>

<!-- <div class="container">
	<div class="pt-5">
		<h2>Scenario definition</h2>
		<form method="POST" action="?/save">
			<textarea
				name="json"
				spellcheck={false}
				rows="10"
				cols="110"
				class="text-sm font-mono textarea textarea-bordered font-mono textarea-lg"
				bind:value={raw}
			></textarea>
			<p>
				<button class="btn btn-primary">Save</button>
			</p>
		</form>
	</div>
</div> -->

<div class="container pt-5">
	<h2>Image library</h2>
	<div class="grid grid-cols-4 gap-4">
		{#each images as image}
			{#if image != null}
				<div class="card lg:card-side card-bordered bg-base-200">
					<figure>
						<img src="{image_url}{image}" style="max-width:100px" />
					</figure>
					<div class="card-body">
						<p class="card-title text-xs">{image}</p>
						<form method="POST" action="?/removeImage">
							<input type="hidden" name="image" value={image} />
							<button
								on:click={confirmRemove}
								type="submit"
								class="btn btn-outline btn-xs btn-error"
								><svg
									xmlns="http://www.w3.org/2000/svg"
									class="h-6 w-6"
									fill="none"
									viewBox="0 0 24 24"
									stroke="currentColor"
									><path
										stroke-linecap="round"
										stroke-linejoin="round"
										stroke-width="2"
										d="M6 18L18 6M6 6l12 12"
									/></svg
								></button
							>
						</form>
					</div>
				</div>
			{/if}
		{/each}
	</div>
</div>

<div class="container pt-5">
	<h2>Upload new image</h2>
	<form method="POST" action="?/upload" enctype="multipart/form-data">
		<input
			class="input input-lg px-0"
			type="file"
			name="file"
			required
			accept=".jpg, .jpeg, .png"
		/>
		<p>
			<button class="btn btn-primary">Upload</button>
		</p>
	</form>
</div>

<style>
	/* textarea {
        width:100%;
        white-space: pre;
        overflow-wrap: normal;
        overflow-x: scroll;        
    } */

</style>
