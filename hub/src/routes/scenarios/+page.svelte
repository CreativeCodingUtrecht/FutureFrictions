<script lang="ts">
	import type { PageData } from './$types';
	import { Avatar } from '@skeletonlabs/skeleton';
	import { goto } from '$app/navigation';

	export let data: PageData;

	const confirmCreate = (e) => {
		if (!confirm('Are you sure you want to create a new scenario? 🤔')) {
			e.preventDefault();
		}
	};
</script>

<!-- <h1 class="h1">Scenarios</h1> -->

<div class="w-full text-token grid grid-cols-1 md:grid-cols-2 gap-4">
	<div class="card card-hover p-4 space-y-4">
		<h4 class="h4">Create new scenario</h4>
		<form method="POST" action="?/create">
			<div class="space-y-4">
			<label>
				Slug
				<input class="input" type="text" required name="slug" />
			</label>
			<label>
				Name
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
                    : `/placeholders/background.jpg`}
                />
		</header>
		<div class="p-4 space-y-4">
			<h3 class="h3" data-toc-ignore>{data.scenarios[scenario].friction.description}</h3>
			<!-- <h6 class="h6" data-toc-ignore>Scenario</h6> -->
			<!-- <Avatar src={data.scenarios[scenario].friction.avatar
				? `/api/scenarios/${scenario}/${data.scenarios[scenario].friction.avatar}`
				: `/placeholders/avatar.jpg`} width="w-32"
				background="white" />
				 -->
			<article>
				<p class="py-5">
					<!-- cspell:disable -->
					{data.scenarios[scenario].scene.content.welcome}
					<!-- cspell:enable -->
				</p>
                <!-- <p class="italic">
                    {data.scenarios[scenario].friction.content.before}
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
