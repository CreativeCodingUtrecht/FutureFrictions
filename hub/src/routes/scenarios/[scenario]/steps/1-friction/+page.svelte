<script lang="ts">
	import { Avatar, FileDropzone } from '@skeletonlabs/skeleton';
import type { PageData, ActionData } from './$types';

	export let form: ActionData;
	export let data: PageData;

	const values = {
		statement: form?.statement || data?.json.scene.content.welcome || '',
		avatar : form?.avatar || data?.json.scene.avatar || ""
	};
</script>

<div>
	<form method="POST" action="?/save">
		<div class="space-y-4">
			<label class="label space-y-4">
				<p>
					<span>
						Fill out this text box with a 'frictional statement' you would like to explore further.
						Feel free to choose one from this list, or come up with yours.
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
					rows="3"
					name="statement"
				/>
			</label>

			<p>
				Examples of frictional statements: 
			</p>				
			<ul class="list">
				<li><span>🌿</span><span class="flex-auto">Implementing high-taxes on single-use plastics to finance urban green spaces</span></li>
				<li><span>💰</span><span class="flex-auto">Implementing a universal basic income to counter automation-induced unemployment</span></li>
				<li><span>🧹</span><span class="flex-auto">Using AI technology to reduce street litter</span></li>
				<li><span>🚨</span><span class="flex-auto">Using sensors to control undesirable behaviors in the city</span></li>					
			</ul>

			<br />

			<label class="label space-y-4">
				<span>What character introduces this frictional statement?</span>
	
				<div class="image-upload-panel">

					{#if values.avatar}
					<img id="preview" src={`/api/scenarios/${data?.scenario}/${values.avatar}`} alt={values.avatar} />
					{/if}

					<FileDropzone 
						name="image"
						on:click={
							(e) => {
								e.preventDefault();
								console.log("Open modal and push selection to the input");
							}
						} 
						on:change={
							(files) => { 
								console.log(files);
								// Upload file and update input
							}
						}
					/>
					
					<input
						value={values.avatar}
						class="input"
						title="Avatar"
						type="text"
						name="avatar"
					/>
					<button type="button" class="btn variant-filled" on:click={() => { console.log("Clear input value") }}>clear</button>
				</div>
			</label>
	
		</div>

		<br />
		<button class="btn variant-filled-primary">Save</button>
		&nbsp;&nbsp;&nbsp;&nbsp; 
		<button class="btn variant-ghost-primary" formaction="?/previous">Back</button>		
		<button class="btn variant-ghost-primary" formaction="?/next">Next</button>
	</form>
</div>

<style>
	.image-upload-panel {
		max-width: 20%;
	}
</style>