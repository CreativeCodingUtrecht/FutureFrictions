<script lang="ts">
	import type { PageData, ActionData } from './$types';

	export let form: ActionData;
	export let data: PageData;

	const values = {
		title : form?.title || data?.json.name || ""
	}
</script>

<div>
	<form 
		method="POST"
		action="?/save">

		<label class="label space-y-4">

			<span>
				<h4 class="h4">Let's setup our scenario!</h4>				
				<br />
				Think of a name for this scenario. What do you want to explore?
			</span>
			<input
				value={values.title}
				class="input"
				title="Title"
				type="text"
				name="title"
				required
			/>
			
			<span class="italic">
				<br />	
				In the next steps you will further define and visualise this scenario by formulating a story around it, with a context, plot and characters. 
			</span>
			
		</label>

		<br />
		<button class="btn variant-filled-primary">Save</button>
		&nbsp;&nbsp;&nbsp;&nbsp; 
		<button class="btn variant-filled-primary" formaction="?/previous">Back</button>		
		<button class="btn variant-filled-primary" formaction="?/next">Next</button>		

	</form>
</div>
