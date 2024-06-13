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

			
			<!-- <h4 class="h4">Futures Wheel</h4>					

			<p>

				<span>
					Brainstorm the direct and indirect consequences of that What If question 10 years from
						now, in the context you have chosen, for the actors you are exploring and beyond. Take a
						sheet of paper and brainstorm these consequences using the Futures Wheel. What are the
						three most thought-provoking impacts?
				</span>
			</p>	
			<div class="card bg-surface-200/30 p-4 space-y-4" >
				<img src="/images/futureswheel.gif" class="px-5" alt="Futures Wheel" />
				<span class="text-sm"><br /><center>Source: <a href="https://matthewlarn.medium.com/explore-direct-indirect-consequences-using-a-futures-wheel-213feb0915a1" class="anchor" target="_new">Explore Direct & Indirect Consequences Using a Futures Wheel</a></center></span>
			</div> -->
		

			<h4 class="h4">Character</h4>
			<span class="label space-y-4">
				<span>What human/non-human character represents this What If question?</span>
				<br />
				<ImageSelector scenario={scenario} images={images} values={values} />			
			</span>

		</div>

		<br />
		<button class="btn variant-filled-primary">Save</button>
		&nbsp;&nbsp;&nbsp;&nbsp;
		<button class="btn variant-filled-primary" formaction="?/previous">Back</button>
		<button class="btn variant-filled-primary" formaction="?/next">Next</button>
	</form>
</div>
