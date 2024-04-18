<script lang="ts">
	import type { PageData, ActionData } from './$types';
	import ImageSelector from '$lib/components/ImageSelector.svelte';
	export let form: ActionData;
	export let data: PageData;

	const images = data?.images;
	const scenario = data?.scenario;

	const values = {
		whatif: form?.whatif || data?.json.friction.content.before || '',
		avatar: form?.avatar || data?.json.friction.avatar || '',
	};
</script>

<div>
	<form method="POST" action="?/save">
		<div class="space-y-4">
			<label class="label space-y-4">
				<p>
					<span>
						Think of a near future (10 years for now) and transform your thought-provoking friction
						into a WHAT IF question To formulate it, consider the friction in its context, and the
						relationship with the characters you defined. (for example: what if local municipalities
						allocate robots in each neighborhood to help reduce street litter?) .
					</span>
				</p>

				<textarea
					value={values.whatif}
					class="textarea"
					title="What if question"
					rows="3"
					name="whatif"
				/>
			</label>

			<p>
				<span>
					<i
						>Brainstorm the direct and indirect consequences of that What If question 10 years from
						now, in the context you have chosen, for the actors you are exploring and beyond. Take a
						sheet of paper and brainstorm these consequences using the Futures Wheel. What are the
						three most thought-provoking impacts?</i
					>
				</span>
			</p>

			<span class="label space-y-4">
				<span>What character represents this What If question?</span>
	
				<ImageSelector scenario={scenario} images={images} values={values} />			
			</span>

		</div>

		<br />
		<button class="btn variant-filled-primary">Save</button>
		&nbsp;&nbsp;&nbsp;&nbsp;
		<button class="btn variant-ghost-primary" formaction="?/previous">Back</button>
		<button class="btn variant-ghost-primary" formaction="?/next">Next</button>
	</form>
</div>
