<script lang="ts">
	import type { PageData, ActionData } from './$types';

	export let form: ActionData;
	export let data: PageData;

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
	<form method="POST" action="?/save">
		<div class="space-y-4">
			<label class="label space-y-4">
				<span
					>Explore this frictional statement further. In what context do you think it is most useful
					to explore the friction it t? Think of places and/or activities.</span
				>
				<input
					bind:value={values.collage}
					class="input"
					title="Collage"
					type="text"
					name="collage"
				/>
			</label>

			<p>
				Think of 3 relevant human/non-human 'characters' in the story that may be affected or play a
				role in connection to this friction. What do you think these characters might say about the
				friction?
			</p>

			<!-- Actor 1 -->
			<div class="card">
				<header class="card-header"><b>Character 1</b></header>
				<section class="p-4 space-y-4">
					<label class="label space-y-4">
						<span>What's the name of the first character?</span>
						<input
							value={values.actor1.name}
							class="input"
							title="Name of the actor"
							type="text"
							name="actor1name"
						/>
					</label>

					<label class="label">
						<span>What does this actor say, think, feel?</span>
						<textarea
							value={values.actor1.statement}
							class="textarea"
							title="Actor statement"
							rows="3"
							name="actor1statement"
						/>
					</label>

					<label class="label">
						<span>Avatar of the actor</span>
						<input
							value={values.actor1.avatar}
							class="input"
							title="Image of the actor"
							type="text"
							name="actor1avatar"
						/>
					</label>

					<label class="label">
						<span>Sprite of the actor</span>
						<input
							value={values.actor1.sprite}
							class="input"
							title="Sprite of the actor"
							type="text"
							name="actor1sprite"
						/>
					</label>
				</section>
			</div>

			<!-- Actor 2 -->
			<div class="card">
				<header class="card-header"><b>Character 2</b></header>
				<section class="p-4 space-y-4">
					<label class="label space-y-4">
						<span>What's the name of the second character?</span>
						<input
							value={values.actor2.name}
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

					<label class="label">
						<span>Avatar of the actor</span>
						<input
							value={values.actor2.avatar}
							class="input"
							title="Image of the actor"
							type="text"
							name="actor2avatar"
						/>
					</label>

					<label class="label">
						<span>Sprite of the actor</span>
						<input
							value={values.actor2.sprite}
							class="input"
							title="Sprite of the actor"
							type="text"
							name="actor2sprite"
						/>
					</label>
				</section>
			</div>

			<!-- Actor 3 -->
			<div class="card">
				<header class="card-header"><b>Character 3</b></header>
				<section class="p-4 space-y-4">
					<label class="label space-y-4">
						<span>What's the name of the third character?</span>
						<input
							value={values.actor3.name}
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

					<label class="label">
						<span>Avatar of the actor</span>
						<input
							value={values.actor3.avatar}
							class="input"
							title="Image of the actor"
							type="text"
							name="actor3avatar"
						/>
					</label>

					<label class="label">
						<span>Sprite of the actor</span>
						<input
							value={values.actor3.sprite}
							class="input"
							title="Sprite of the actor"
							type="text"
							name="actor3sprite"
						/>
					</label>
				</section>
			</div>

			<label class="label space-y-4">
				<p>
					<span>
						After expressing the background and characters related to the initial statement, do you see any emerging frictions? Select the most thought-provoking friction you want to explore in the next steps.
					</span>
				</p>

				<textarea
					value={values.emergingfriction}
					class="textarea"
					title="Emerging friction"
					rows="3"
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
