<script lang="ts">
	import type { PageData, ActionData } from './$types';
	import ImageSelector from '$lib/components/ImageSelector.svelte';
	export let form: ActionData;
	export let data: PageData;

	const images = data?.images;
	const scenario = data?.scenario;

	const values = {
		emergingfrictions: form?.emergingfrictions || data?.json.friction?.emergingfrictions || ''
	};
</script>

<div>
	<form method="POST" action="?/save" enctype="multipart/form-data">
		<div class="space-y-4">
			<h4 class="h4">Emerging frictions</h4>			

			<label class="label space-y-4">
				<p>
					<span>
						Contextualise the intervention and the changes it introduces to your scenario. 
						Brainstorm about changes to the context (place/neighbourhood) and characters you visualised earlier. 
						Can you imagine any issues that might arise?
						Describe the most thought-provoking issue/friction you want to visualise in the next step.
					</span>
				</p>
	
				<textarea
					value={values.emergingfrictions}
					class="textarea"
					title="Emerging friction"
					rows="4"
					name="emergingfrictions"
					maxlength="2048"
				/>
			</label> 
		</div>

		<br />
		<button class="btn variant-filled-primary">Save</button>
		&nbsp;&nbsp;&nbsp;&nbsp;
		<button class="btn variant-filled-primary" formaction="?/previous">Back</button>
		<button class="btn variant-filled-primary" formaction="?/next">Next</button>
	</form>
</div>
