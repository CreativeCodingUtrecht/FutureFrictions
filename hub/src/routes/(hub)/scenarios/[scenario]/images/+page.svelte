<script lang="ts">
	import type { PageData } from './$types';

	export let data: PageData;

	const scenario = data.scenario;
	let json = data.json;

	const images = data.images;

	const image_url = `/api/scenarios/${scenario}/`;

	const confirmRemove = (e) => {
		if (!confirm('Are you sure you want to remove this image? 🤔')) {
			e.preventDefault();
		}
	};

	const debuggie = () => {
		console.log(json);
	};
</script>

<h2 class="h2">Image library</h2>
<br />
<div class="w-full text-token grid grid-cols-2 md:grid-cols-4 gap-4">
	<div class="card card-hover p-4 space-y-4">
		<h4 class="h4">Upload image</h4>

		<form method="POST" action="?/upload" enctype="multipart/form-data">
			<div class="space-y-4">
				<input class="input" type="file" name="file" required accept=".jpg, .jpeg, .png" />
				<p>
					<button class="btn variant-filled-primary">Upload</button>
				</p>
			</div>
		</form>
	</div>
	{#each images as image}
		{#if image != null}
			<div class="card card-hover p-4 space-y-4">
				<header>
					<img src="{image_url}{image}" style="max-height:200px" />
				</header>
				<div class="p-4 space-y-4">
					<p class="text-xs">{image}</p>
				</div>
				<hr class="opacity-50" />
				<footer class="p-4 flex justify-start items-center space-x-4">
					<div class="flex-auto flex justify-between items-center">
						<form method="POST" action="?/removeImage">
							<input type="hidden" name="image" value={image} />
							<button on:click={confirmRemove} type="submit" class="btn variant-ghost-error"
								>Delete</button
							>
						</form>
					</div>
				</footer>
			</div>
		{/if}
	{/each}
</div>
