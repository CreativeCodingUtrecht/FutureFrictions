<script lang="ts">
	import {v4 as uuidv4} from 'uuid';
	import type { PageData } from './$types';
	import Play from '$lib/components/Play.svelte';

	export let data: PageData;

	const confirmCreate = (e) => {
		if (!confirm('Are you sure you want to create a new scenario? 🤔')) {
			e.preventDefault();
		}
	};
</script>

<h4 class="h4"><a href="/scenarios">Scenarios</a></h4>

<div class="w-full text-token grid grid-cols-1 md:grid-cols-3 gap-4">	
	<div class="card card-hover p-4 space-y-4">
		<h4 class="h4">Create your own scenario</h4>
		<p>
			Start exploring a new situation by <b>creating a new scenario</b>,
			or by opening and <b>duplicating an existing scenario</b> to experiment with different outcomes.
		</p>
		<form method="POST" action="?/create">
			<div class="space-y-4">
			<input class="input" value={uuidv4()} type="hidden" required name="slug" />
			<label>
				Name of your scenario
				<input class="input" type="text" required name="name" />
			</label>
	
			<button on:click={confirmCreate} type="submit" class="btn variant-filled-primary">Create</button>
		</div>
		</form> 	
	</div>
{#each Object.keys(data.scenarios) as scenario}
	<div class="card card-hover overflow-hidden">
		<header>
			<a href="/scenarios/{scenario}">
			<img
                src={data.scenarios[scenario].collage?.future?.url || data.scenarios[scenario].collage?.present?.url || ''}
                />
			</a>
		</header>
		<div class="p-4 space-y-4">
			<h4 class="h4" data-toc-ignore>{data.scenarios[scenario].name}</h4>
			<span class="text-xs">Created by <b>{data.scenarios[scenario].author}</b></span>
			<article>
				<p>
					<Play {scenario} />
					<a href="/scenarios/{scenario}" class="btn variant-ghost-primary">Open</a>
					<a href="/scenarios/{scenario}/steps/0-setup" class="btn variant-ghost-primary">Edit</a>		
				</p>
			</article>
		</div>
	</div>
{/each}
</div>
