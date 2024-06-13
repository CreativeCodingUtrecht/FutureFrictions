<script lang="ts">
	import type { PageData, ActionData } from './$types';
	import ImageSelector from '$lib/components/ImageSelector.svelte';

	export let form: ActionData;
	export let data: PageData;

	const images = data?.images;
	const scenario = data?.scenario;

	const values = {
		statement: form?.statement || data?.json.friction?.frictionalstatement || '',
		avatar : form?.avatar || data?.json.friction?.avatar || ""
	};
</script>

<div>
	<form method="POST" action="?/save" enctype="multipart/form-data">
		<div class="space-y-4">
			<h4 class="h4">Frictional statement</h4>			
			<label class="label space-y-4">
				<p>
					<span>
						Fill out this text box with a <b>frictional statement</b> you would like to explore further.
						Feel free to copy one from this list, or come up with yours.
					</span>
				</p>

				<p>
					<span>
						<i>A frictional statement refers to an existing trend, change or vision that is potentially controversial and leads to dilemmas.</i>
					</span>
				</p>

				<textarea
					value={values.statement}
					class="textarea"
					title="Frictional statement"
					rows="4"
					name="statement"
				/>

				<p>
					Examples of frictional statements: 
				</p>				
				<ul class="list">
					<li><span>🌿</span><span class="flex-auto">Implementing high-taxes on single-use plastics to finance urban green spaces</span></li>
					<li><span>💰</span><span class="flex-auto">Implementing a universal basic income to counter automation-induced unemployment</span></li>
					<li><span>🧹</span><span class="flex-auto">Using AI technology to reduce street litter</span></li>
					<li><span>🚨</span><span class="flex-auto">Using sensors to control undesirable behaviors in the city</span></li>					
				</ul>
			</label>

			<h4 class="h4">Character</h4>
		
			<span class="label space-y-4">
				<br />
				<span>Think of a human/non-human character that introduces this frictional statement.</span>
				<ImageSelector title="Create a character" field="avatar" {scenario} {images} {values} />
			</span>
	
		</div>

		<br />
		<button class="btn variant-filled-primary">Save</button>
		&nbsp;&nbsp;&nbsp;&nbsp; 
		<button class="btn variant-filled-primary" formaction="?/previous">Back</button>		
		<button class="btn variant-filled-primary" formaction="?/next">Next</button>
	</form>
</div>
