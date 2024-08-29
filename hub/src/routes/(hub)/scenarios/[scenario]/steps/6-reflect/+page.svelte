<script lang="ts">
	import type { PageData, ActionData } from './$types';
	import { FF_WEBGL_URL } from '$lib/constants';
	import Play from '$lib/components/Play.svelte';

	export let form: ActionData;
	export let data: PageData;

	const scenario = data.scenario;

	const values = {
		provocativestatement : form?.provocativestatement || data?.json.provocativestatement || ''
	};
</script>

<div>
	<form method="POST" action="?/save">
		<h4 class="h4">Now, let's see the outcome of our future!</h4>				
		<br />
		<p>
			Press the <b>Explore</b> button to see the outcome of the future and a sequence of what you have created.
		</p>
		<br />
		<Play {scenario} />
		<br />

		<br />
		<h4 class="h4">Provocative statement</h4>		
		<br />

		<label class="label space-y-4">
			<span>
				What is the question you would like to ask others while presenting this future, what is your provocative statement?
			</span>
			<textarea
				value={values.provocativestatement}
				class="textarea"
				title="Provocative Statement"
				rows="4"
				name="provocativestatement"
			/>
		</label>

		<br />
		<button class="btn variant-filled-primary">Save</button>
		&nbsp;&nbsp;&nbsp;&nbsp; 
		<button class="btn variant-filled-primary" formaction="?/previous">Back</button>		
		<button class="btn variant-filled-primary" formaction="?/complete">Complete</button>
	</form>
</div>
