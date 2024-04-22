<script lang="ts">
	import type { PageData } from './$types';
	import {v4 as uuidv4} from 'uuid';

	import { FF_WEBGL_URL } from '$lib/constants';

	export let data: PageData;

	const scenario = data.scenario;
	const json = data.json;

	const title = json.friction.description;
	const welcome = json.scene.content.welcome;	
	const background = json.scene.background;
	const avatar = json.scene.avatar;

	const frictionalstatement = data?.json?.friction?.content.before || '';
	const emergingfrictions = data?.json?.scene?.content.emergingfriction || '';
	const provocativestatement = data?.json.scene.content.provocativestatement || ''

	const background_url = background
		? `/api/scenarios/${scenario}/${background}`
		: ``;
	const avatar_url = avatar ? `/api/scenarios/${scenario}/${avatar}` : `/placeholders/avatar.jpg`;

	const url = `${FF_WEBGL_URL}?scenario=${scenario}`;

	const confirmRemove = (e) => {
		if (!confirm('Are you sure you want to delete this scenario? 🤔')) {
			e.preventDefault();
		}
	};

	const confirmDuplicate = (e) => {
		if (!confirm('Are you sure you want to duplicate this scenario? 🤔')) {
			e.preventDefault();
		}
	};
</script>

<div class="w-full text-token grid grid-cols-1 md:grid-cols-1 gap-4">
	<div class="card overflow-hidden">
		<header>
			<img src={background_url} />
		</header>
		<div class="p-4 space-y-4">
			<h3 class="h3" data-toc-ignore>{title}</h3>
			<article class="space-y-5">
				{#if welcome}
					<h5 class="h5">Frictional statement</h5>
					<p>						
						{welcome}
					</p>
				{/if}
				{#if emergingfrictions}
					<h5 class="h5">Emerging frictions</h5>
					<p>
						{emergingfrictions}
					</p>
				{/if}
				{#if frictionalstatement}
					<h5 class="h5">What If ..</h5>
					<p>
						{frictionalstatement}
					</p>
				{/if}
				{#if provocativestatement}
					<h5 class="h5">Provocative statement</h5>
					<p>
						{provocativestatement}
					</p>
				{/if}
			</article>
            <form method="POST" action="?/remove">
				<a href={url} class="btn variant-filled-primary" target="FF_WEBGL_URL">▶ Play</a>
				<a href="/scenarios/{scenario}/steps/0-setup" class="btn variant-filled-primary">Edit</a>	
				<button on:click={confirmRemove} type="submit" class="btn variant-ghost-error">Remove</button>
            </form>        
		</div>
	</div>
	<div class="card overflow-hidden">
		<div class="p-4 space-y-4">
            <h4 class="h4">Duplicate scenario</h4>
            <form method="POST" action="?/duplicate">
                <div class="space-y-4">
				<input class="input" value={uuidv4()} type="hidden" required name="slug" />

                <label>
                    Name of your scenario
                    <input
                        class="input"
                        type="text"
                        required
                        value={`${title} (Copy)`}
                        name="name"
                    />
                </label>
        
                <button on:click={confirmDuplicate} type="submit" class="btn variant-ghost-primary">Duplicate</button>
                </div>
            </form>


        </div>
	</div>
</div>
