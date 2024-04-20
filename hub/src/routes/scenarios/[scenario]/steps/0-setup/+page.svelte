<script lang="ts">
	import type { PageData, ActionData } from './$types';

	export let form: ActionData;
	export let data: PageData;

	const values = {
		title : form?.title || data?.json.friction.description || ""
	}
</script>

<div>
	<form 
		method="POST"
		action="?/save">

		<label class="label space-y-4">

			<span>
				<h4 class="h4">Scenario title</h4>				
				<br />
				What shall we call this scenario?
			</span>
			<input
				value={values.title}
				class="input"
				title="Title"
				type="text"
				name="title"
			/>
		</label>

		<br />
		<button class="btn variant-filled-primary">Save</button>
		&nbsp;&nbsp;&nbsp;&nbsp; 
		<button class="btn variant-ghost-primary" formaction="?/previous">Back</button>		
		<button class="btn variant-ghost-primary" formaction="?/next">Next</button>		

	</form>
</div>
