<script lang="ts">
	import type { PageData, ActionData } from './$types';

	export let form: ActionData;
	export let data: PageData;
	
	const values = {
		whatif: form?.whatif || data?.json.whatif?.question || ''
	};
</script>

<div>
	<form method="POST" action="?/save" enctype="multipart/form-data">
		<div class="space-y-4">
			<h4 class="h4">Intervention</h4>			
			<label class="label space-y-4">
				<p>
					<span>
						Think of an intervention that will change the initial situation.
						This could be an actual intervention you are aware of or a speculative/imaginative one.
						<br /><br />
						Think of a thought-provoking <b>what if?</b> question or statement that describes how this intervention 
						might change the world. You may also think about who might introduce this intervention to resolve 
						the situation you defined earlier. 
					</span>
				</p>

				<textarea
					value={values.whatif}
					class="textarea"
					title="What if question"
					rows="4"
					name="whatif"
					maxlength="2048"
				/>
				<br />
				<i>For example: "What if local municipalities
				allocate robots in each neighborhood to help reduce street litter?"</i>
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
