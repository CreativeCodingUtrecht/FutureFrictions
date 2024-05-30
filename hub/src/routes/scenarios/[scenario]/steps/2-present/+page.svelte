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
		emergingfrictions: form?.emergingfrictions || data?.json.friction?.emergingfrictions || '',
		file: undefined
	};

	$: stringifiedCollage = () => {
		return JSON.stringify(values.collage);
	}

	$: stringifiedDefinition = () => {
		return JSON.stringify(values.definition);
	}

	const handleSubmit = async () => {
		console.log('Submitting');
		if (values.file) {
			let fileInputElement = document.getElementById('file');		
			let container = new DataTransfer();
			container.items.add(values.file);
			fileInputElement.files = container.files;
		}
	}

</script>

<div>
	<form on:submit={handleSubmit} method="POST" action="?/save" enctype="multipart/form-data">
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
		<Collage {scenario} {backgrounds} {elements} {characters} bind:collage={values.collage} bind:definition={values.definition} bind:file={values.file}/>
		<input type="hidden" name="collage" value={stringifiedCollage()} />
		<input type="hidden" name="definition" value={stringifiedDefinition()} />
		<input id="file" type="file" name="collageFile" bind:value={values.file} />
		<br />
		<div class="space-y-4">
			<h4 class="h4">Characters</h4>
			<p>
				What do you think the characters might say about the friction?
			</p>

			{#each values.definition.characters as character, i}

				<!-- Actor 1 -->
				<div class="card">
					<header class="card-header">
						<img src={character.url} alt={`Character ${i}`} width="100" />
					</header>
					<section class="p-4 space-y-4">
						<label class="label space-y-4">
							<span>What's the name of this character?</span>
							<input
								bind:value={character.name}
								class="input"
								title="Name of the actor"
								type="text"
								name="actor1name"
							/>
						</label>

						<label class="label">
							<span>What does this character say, think, feel?</span>
							<textarea
								bind:value={character.statement}
								class="textarea"
								title="Actor statement"
								rows="3"
								name="actor1statement"
							/>
						</label>
					</section>
				</div>
			{/each}
		</div>

		<br />

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
				value={values.emergingfrictions}
				class="textarea"
				title="Emerging friction"
				rows="4"
				name="emergingfrictions"
			/>
		</label>

		<br />
		<button class="btn variant-filled-primary">Save</button>
		&nbsp;&nbsp;&nbsp;&nbsp;
		<button class="btn variant-filled-primary" formaction="?/previous">Back</button>
		<button class="btn variant-filled-primary" formaction="?/next">Next</button>
	</form>
</div>

<style>
	input#file {
		display: none;
	}
</style>
