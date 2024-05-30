<script lang="ts">
	import type { PageData, ActionData } from './$types';
	import Collage from '$lib/components/Collage.svelte';

	export let form: ActionData;
	export let data: PageData;

	const scenario = data?.scenario;
	const backgrounds = data?.backgrounds;
	const elements = data?.elements;
	const characters = data?.characters;

	const values = {
		collage: data?.json?.collage?.present?.canvas || {},
		definition: data?.json?.collage?.present?.definition || {},
		emergingfriction: form?.emergingfriction || data?.json.friction?.emergingfriction || ''
	};

	$: stringifiedCollage = () => {
		return JSON.stringify(values.collage);
	}

	$: stringifiedDefinition = () => {
		return JSON.stringify(values.definition);
	}
</script>

<div>
	<form method="POST" action="?/save" enctype="multipart/form-data">
		<div class="">
			<h4 class="h4">Collage</h4>
			<br />
			<span
				>Explore this frictional statement further. In what context do you think it is most useful
				to explore the friction? Think of places and/or activities.<br />
				Also think of 3 relevant human/non-human 'characters' in the story that may be affected or play a
				role in connection to this friction. 
			</span>			

		</div>
		<br />		
		<Collage {scenario} {backgrounds} {elements} {characters} bind:collage={values.collage} bind:definition={values.definition}/>
		<input type="hidden" name="collage" value={stringifiedCollage()} />
		<input type="hidden" name="definition" value={stringifiedDefinition()} />
		<!-- <input type="hidden" name="collageFile" bind:value={values.blob} /> -->
		<br />
		{#if values.definition.characters?.length > 0}
		<div class="space-y-4">
			<h4 class="h4">Characters</h4>
			<p>
				What do you think the characters might say about the friction?
			</p>

			<!-- Actor 1 -->
			<div class="card">
				<!-- <header class="card-header">
					<b>{values.actor1.name ? values.actor1.name : 'Character 1'}</b>
				</header> -->
				<section class="p-4 space-y-4">

					<img src={values.definition.characters[0].src} alt="Character 1" width="100" />

					<label class="label space-y-4">
						<span>What's the name of this character?</span>
						<input
							bind:value={values.definition.characters[0].name}
							class="input"
							title="Name of the actor"
							type="text"
							name="actor1name"
						/>
					</label>

					<label class="label">
						<span>What does this character say, think, feel?</span>
						<textarea
							bind:value={values.definition.characters[0].statement}
							class="textarea"
							title="Actor statement"
							rows="3"
							name="actor1statement"
						/>
					</label>
				</section>
			</div>

			<!-- Actor 2 -->
			{#if values.definition.characters?.length > 1}			
			<div class="card">
				<!-- <header class="card-header">
					<b>{values.actor2.name ? values.actor2.name : 'Character 2'}</b>
				</header> -->
				<section class="p-4 space-y-4">

					<img src={values.definition.characters[1].src} alt="Character 2" width="100" />

					<label class="label space-y-4">
						<span>What's the name of this character?</span>
						<input
							bind:value={values.definition.characters[1].name}
							class="input"
							title="Name of the actor"
							type="text"
							name="actor2name"
						/>
					</label>

					<label class="label">
						<span>What does this actor say, think, feel?</span>
						<textarea
							bind:value={values.definition.characters[1].statement}
							class="textarea"
							title="Actor statement"
							rows="3"
							name="actor2statement"
						/>
					</label>
				</section>
			</div>
			{/if}

			<!-- Actor 3 -->
			{#if values.definition.characters?.length > 2}			
			<div class="card">
				<!-- <header class="card-header">
					<b>{values.actor3.name ? values.actor3.name : 'Character 3'}</b>
				</header> -->
				<section class="p-4 space-y-4">				

					<img src={values.definition.characters[2].src} alt="Character 3" width="100" />

					<label class="label space-y-4">
						<span>What's the name of this character?</span>
						<input
							bind:value={values.definition.characters[2].name}
							class="input"
							title="Name of the actor"
							type="text"
							name="actor3name"
						/>
					</label>

					<label class="label">
						<span>What does this actor say, think, feel?</span>
						<textarea
							bind:value={values.definition.characters[2].statement}
							class="textarea"
							title="Actor statement"
							rows="3"
							name="actor3statement"
						/>
					</label>
				</section>
			</div>
			{/if}
			<br />

		</div>
		{/if}

		<label class="label space-y-4">
			<h4 class="h4">Emerging frictions</h4>
			<p>
				<span>
					After expressing the background and characters related to the initial statement, do you
					see any emerging frictions? Select the most thought-provoking friction you want to
					explore in the next steps.
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

		<br />
		<button class="btn variant-filled-primary">Save</button>
		&nbsp;&nbsp;&nbsp;&nbsp;
		<button class="btn variant-filled-primary" formaction="?/previous">Back</button>
		<button class="btn variant-filled-primary" formaction="?/next">Next</button>
	</form>
</div>
