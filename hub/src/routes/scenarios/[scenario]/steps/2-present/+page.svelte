<script lang="ts">
	import type { PageData, ActionData } from './$types';
	import ImageSelector from '$lib/components/ImageSelector.svelte';

	export let form: ActionData;
	export let data: PageData;
	
	const images = data?.images;
	const scenario = data?.scenario;

	const values = {
		collage: form?.collage || data?.json.scene.background || '',
		actor1: {
			name: form?.actor1.name || data?.json.actors[0].description || '',
			statement: form?.actor1.statement || data?.json.actors[0].content.before || '',
			avatar: form?.actor1.avatar || data?.json.actors[0].avatar || '',
			sprite: form?.actor1.sprite || data?.json.actors[0].sprite || ''
		},
		actor2: {
			name: form?.actor2.name || data?.json.actors[1].description || '',
			statement: form?.actor2.statement || data?.json.actors[1].content.before || '',
			avatar: form?.actor2.avatar || data?.json.actors[1].avatar || '',
			sprite: form?.actor2.sprite || data?.json.actors[1].sprite || ''
		},
		actor3: {
			name: form?.actor3.name || data?.json.actors[2].description || '',
			statement: form?.actor3.statement || data?.json.actors[2].content.before || '',
			avatar: form?.actor3.avatar || data?.json.actors[2].avatar || '',
			sprite: form?.actor3.sprite || data?.json.actors[2].sprite || ''
		},
		emergingfriction: form?.emergingfriction || data?.json.scene.content.emergingfriction || ''
	};
</script>

<div>
	<form method="POST" action="?/save" enctype="multipart/form-data">
		<div class="">
			<h4 class="h4">Collage</h4>
			<br />
			<span>
				<span
					>Explore this frictional statement further. In what context do you think it is most useful
					to explore the friction? Think of places and/or activities.</span
				>
				<ImageSelector field="collage" upload="collage_image" {scenario} {images} {values} extraClass="" />			
			</span>
		</div>
		<br />
		<div class="space-y-4">
			
			<h4 class="h4">Characters</h4>
			<p>
				Think of 3 relevant human/non-human 'characters' in the story that may be affected or play a
				role in connection to this friction. What do you think these characters might say about the
				friction?
			</p>

			<!-- Actor 1 -->
			<div class="card">
				<header class="card-header"><b>{values.actor1.name ? values.actor1.name : "Character 1"}</b></header>
				<section class="p-4 space-y-4">

					<!-- <ImageSelector 
						title="Avatar" 
						field="avatar"
						input="actor1avatar" 
						{scenario} 
						{images} 
						upload="actor1avatarFile"
						values={values.actor1} /> -->

					<ImageSelector 
						title="Sprite" 
						field="sprite"
						input="actor1sprite" 
						{scenario} 
						{images} 
						upload="actor1spriteFile"
						values={values.actor1} />

					<label class="label space-y-4">
						<span>What's the name of the first character?</span>
						<input
							bind:value={values.actor1.name}
							class="input"
							title="Name of the actor"
							type="text"
							name="actor1name"
						/>
					</label>

					<label class="label">
						<span>What does this character say, think, feel?</span>
						<textarea
							value={values.actor1.statement}
							class="textarea"
							title="Actor statement"
							rows="3"
							name="actor1statement"
						/>
					</label>

				</section>
			</div>

			<!-- Actor 2 -->
			<div class="card">
				<header class="card-header"><b>{values.actor2.name ? values.actor2.name : "Character 2"}</b></header>
				<section class="p-4 space-y-4">
					<!-- <ImageSelector 
						title="Avatar" 
						field="avatar"
						input="actor2avatar" 
						{scenario} 
						{images} 
						upload="actor2avatarFile"
						values={values.actor2} /> -->

					<ImageSelector 
						title="Sprite" 
						field="sprite"
						input="actor2sprite" 
						{scenario} 
						{images} 
						upload="actor2spriteFile"
						values={values.actor2} />					

					<label class="label space-y-4">
						<span>What's the name of the second character?</span>
						<input
							bind:value={values.actor2.name}
							class="input"
							title="Name of the actor"
							type="text"
							name="actor2name"
						/>
					</label>

					<label class="label">
						<span>What does this actor say, think, feel?</span>
						<textarea
							value={values.actor2.statement}
							class="textarea"
							title="Actor statement"
							rows="3"
							name="actor2statement"
						/>
					</label>
				</section>
			</div>

			<!-- Actor 3 -->
			<div class="card">
				<header class="card-header"><b>{values.actor3.name ? values.actor3.name : "Character 3"}</b></header>
				<section class="p-4 space-y-4">
					<!-- <ImageSelector 
						title="Avatar" 
						field="avatar" 
						input="actor3avatar" 
						{scenario} 
						{images} 
						upload="actor3avatarFile"
						values={values.actor3} /> -->

					<ImageSelector 
						title="Sprite" 
						field="sprite"
						input="actor3sprite" 
						{scenario} 
						{images} 
						upload="actor3spriteFile"
						values={values.actor3} />

					<label class="label space-y-4">					
						<span>What's the name of the third character?</span>
						<input
							bind:value={values.actor3.name}
							class="input"
							title="Name of the actor"
							type="text"
							name="actor3name"
						/>
					</label>

					<label class="label">
						<span>What does this actor say, think, feel?</span>
						<textarea
							value={values.actor3.statement}
							class="textarea"
							title="Actor statement"
							rows="3"
							name="actor3statement"
						/>
					</label>
				</section>
			</div>
			<br />
			
			<label class="label space-y-4">

				<h4 class="h4">Emerging frictions</h4>
				<p>
					<span>
						After expressing the background and characters related to the initial statement, do you see any emerging frictions? Select the most thought-provoking friction you want to explore in the next steps.
					</span>
				</p>

				<textarea
					value={values.emergingfriction}
					class="textarea"
					title="Emerging friction"
					rows="4"
					name="emergingfriction"
				/>
			</label>			
		</div>

		<br />
		<button class="btn variant-filled-primary">Save</button>
		&nbsp;&nbsp;&nbsp;&nbsp;
		<button class="btn variant-ghost-primary" formaction="?/previous">Back</button>
		<button class="btn variant-ghost-primary" formaction="?/next">Next</button>
	</form>
</div>
