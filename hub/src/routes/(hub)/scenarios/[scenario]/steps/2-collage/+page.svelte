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
			<p>
				Explore this situation further by visualising a context and defining main characters. <br />
			</p>
			<br />
			<ul class="list">
				<li><span>🏞️</span><span class="flex-auto">When creating the context, think of places and/or activities.</span></li>
				<li><span>👭</span><span class="flex-auto">Think of at least 2 relevant human/non-human 'characters' in the story that may be affected or play a
					role in connection to the situation you are exploring.</span></li>
				<li><span>🏕️</span><span class="flex-auto">You may choose and add to the context also other secondary characters and ambient elements that allow you to depict a more complete picture.</span></li>
			</ul>
			
		</div>
		<br />
				
		<Collage includeFriction={false} {backgrounds} {elements} {scenario} bind:collage={values.collage} bind:definition={values.definition} bind:file={values.file}/>
		<input type="hidden" name="collage" value={stringifiedCollage()} />
		<input type="hidden" name="definition" value={stringifiedDefinition()} />
		<input id="file" type="file" name="collageFile" bind:value={values.file} />

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
