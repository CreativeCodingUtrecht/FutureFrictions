<script lang="ts">
	import type { PageData, ActionData } from './$types';

	export let form: ActionData;
	export let data: PageData;

	const values = {
		statement: form?.statement || data?.json.scene.content.welcome || ''
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
		</div>

		<br />
		<button class="btn variant-filled-primary">Save</button>
		&nbsp;&nbsp;&nbsp;&nbsp; 
		<button class="btn variant-ghost-primary" formaction="?/previous">Back</button>		
		<button class="btn variant-ghost-primary" formaction="?/next">Next</button>
	</form>
</div>
