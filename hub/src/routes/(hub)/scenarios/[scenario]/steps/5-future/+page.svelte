<script lang="ts">
	import type { PageData, ActionData } from './$types';
	import Collage from '$lib/components/Collage.svelte';

	export let form: ActionData;
	export let data: PageData;

	const scenario = data?.scenario;
	const backgrounds = data?.backgrounds;
	const elements = data?.elements;

	const values = {
		collage: data?.json.collage?.future?.canvas || data?.json.collage?.present?.canvas || {},
		definition: data?.json.collage?.future?.definition || data?.json?.collage?.present?.definition || {},
		file: undefined
	};

	$: stringifiedCollage = () => {
		return JSON.stringify(values.collage);
	}

	$: stringifiedDefinition = () => {
		return JSON.stringify(values.definition);
	}

	const resetCollage = () => {
		console.log('Resetting collage');
		console.log('Before',values.definition)
		values.collage = data?.json.collage?.present?.canvas || {};
		values.definition = data?.json?.collage?.present?.definition || {};
		console.log('After',values.definition)
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
	<form method="POST" on:submit={handleSubmit} action="?/save" enctype="multipart/form-data">
		<div class="">
			<h4 class="h4">Collage</h4>
			<br />
			<span class="label space-y-4">
				
				<p>
					Visualise the future frictions, by showing changes to the context and the characters.
					How might that future look like? 
					And, what are elements that emerge as changes to the world in this future? Think of characters, conversations, activities, and interactions. You may also introduce new characters.
				</p>
			</span>
			<br />
			<Collage includeFriction={true} {scenario} {backgrounds} {elements} bind:collage={values.collage} bind:definition={values.definition} bind:file={values.file} />
			<input type="hidden" name="collage" value={stringifiedCollage()} />
			<input type="hidden" name="definition" value={stringifiedDefinition()} />
			<input id="file" type="file" name="collageFile" bind:value={values.file} />
			<!-- <br />
			<a class="btn variant-ghost-warning" on:click={resetCollage}>Reset collage</a>			 -->
		</div> 

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
