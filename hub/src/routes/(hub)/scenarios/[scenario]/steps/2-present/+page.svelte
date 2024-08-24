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
		<Collage includeInterventions={false} {backgrounds} {elements} {scenario} bind:collage={values.collage} bind:definition={values.definition} bind:file={values.file}/>
		<input type="hidden" name="collage" value={stringifiedCollage()} />
		<input type="hidden" name="definition" value={stringifiedDefinition()} />
		<input id="file" type="file" name="collageFile" bind:value={values.file} />

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
