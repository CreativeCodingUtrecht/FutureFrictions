<script lang="ts">
	import type { PageData, ActionData } from './$types';

	export let form: ActionData;
	export let data: PageData;

	const values = {
	};
</script>

<div>
	<form method="POST" action="?/save">
		<div class="space-y-4">
		</div>

		<br />
		<button class="btn variant-filled-primary">Save</button>
		&nbsp;&nbsp;&nbsp;&nbsp; 
		<button class="btn variant-ghost-primary" formaction="?/previous">Back</button>		
		<button class="btn variant-ghost-primary" formaction="?/complete">Complete</button>
	</form>
</div>
