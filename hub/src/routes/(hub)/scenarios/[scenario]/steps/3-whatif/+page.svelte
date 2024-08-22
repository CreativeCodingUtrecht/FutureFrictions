<script lang="ts">
	import type { PageData, ActionData } from './$types';
	import ImageSelector from '$lib/components/ImageSelector.svelte';
	export let form: ActionData;
	export let data: PageData;

	const images = data?.images;
	const scenario = data?.scenario;

	const values = {
		whatif: form?.whatif || data?.json.whatif?.question || '',
		avatar: form?.avatar || data?.json.whatif?.avatar || '',
	};
</script>

<div>
	<form method="POST" action="?/save" enctype="multipart/form-data">
		<div class="space-y-4">
			<h4 class="h4">What if .. </h4>			
			<label class="label space-y-4">
				<p>
					<span>
						Think of a near future (10 years for now) and transform your thought-provoking friction
						into a <b>What If question</b>. To formulate it, consider the friction in its context, and the
						relationship with the characters you defined. (for example: what if local municipalities
						allocate robots in each neighborhood to help reduce street litter?) .
					</span>
				</p>

				<textarea
					value={values.whatif}
					class="textarea"
					title="What if question"
					rows="4"
					name="whatif"
				/>
				<br />
			</label>

		</div>

		<br />
		<button class="btn variant-filled-primary">Save</button>
		&nbsp;&nbsp;&nbsp;&nbsp;
		<button class="btn variant-filled-primary" formaction="?/previous">Back</button>
		<button class="btn variant-filled-primary" formaction="?/next">Next</button>
	</form>
</div>
