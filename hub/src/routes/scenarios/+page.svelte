<script lang="ts">
	import {v4 as uuidv4} from 'uuid';
	import type { PageData } from './$types';
	import { FF_WEBGL_URL } from '$lib/constants';

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
			Start exploring a new frictional statement by <b>creating a new scenario</b>,
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
	<a class="card card-hover overflow-hidden" href="/scenarios/{scenario}">
		<header>
			<img
                src={data.scenarios[scenario].scene.background
                    ? `/api/scenarios/${scenario}/${data.scenarios[scenario].scene.background}`
                    : ``}
                />
		</header>
		<div class="p-4 space-y-4">
			<h4 class="h4" data-toc-ignore>{data.scenarios[scenario].friction.description}</h4>
			<!-- <h6 class="h6" data-toc-ignore>Scenario</h6> -->
			<!-- <Avatar src={data.scenarios[scenario].friction.avatar
				? `/api/scenarios/${scenario}/${data.scenarios[scenario].friction.avatar}`
				: `/placeholders/avatar.jpg`} width="w-32"
				background="white" />
				 -->
			<article>
				<p>
					<a href="/scenarios/{scenario}" class="btn variant-filled-primary">Open</a>
					<a href={`${FF_WEBGL_URL}?scenario=${scenario}`} class="btn variant-ghost-primary" target="FF_WEBGL_URL">▶ Play</a>
					<a href="/scenarios/{scenario}/steps/0-setup" class="btn variant-ghost-primary">Edit</a>		
				</p>
				<p class="py-5">
					<!-- cspell:disable -->
					{data.scenarios[scenario].scene.content.welcome}
					<!-- cspell:enable -->
				</p>
                <!-- <p class="italic">
                    {data.scenarios[scenario].friction.content.provocativestatement}
                </p> -->
			</article>
		</div>
		<!-- <hr class="opacity-50" />
		<footer class="p-4 flex justify-start items-center space-x-4">
			<div class="flex-auto flex justify-between items-center">
				<h6 class="font-bold" data-toc-ignore>Boom</h6>
			</div>
		</footer> -->
	</a>
{/each}
</div>
<!-- 

<div class="grid grid-cols-4 gap-4">

		<div class="card bg-base-200 shadow-xl image-full">
			<figure>
			</figure>
			<div class="card-body">
				<h2 class="card-title"></h2>
				<div class="card-actions justify-end">
					<a href= class="btn btn-primary">Open</a>
				</div>
			</div>
		</div>
	{/each}
</div> -->
