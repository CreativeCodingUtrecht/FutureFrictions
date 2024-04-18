<script lang="ts">
	import type { PageData } from './$types';
	import { FF_WEBGL_URL } from '$lib/constants';

	export let data: PageData;

	const scenario = data.scenario;
	const json = data.json;

	const title = json.friction.description;
	const welcome = json.scene.content.welcome;
	const background = json.scene.background;
	const avatar = json.scene.avatar;

	const background_url = background
		? `/api/scenarios/${scenario}/${background}`
		: `/placeholders/background.jpg`;
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
			<article>
				<p class="py-5">
					{welcome}
				</p>
			</article>
			<a href={url} class="btn variant-filled-primary" target="FF_WEBGL_URL">Play</a>
			<a href="/scenarios/{scenario}/steps/0-setup" class="btn variant-filled-primary">Edit</a>
            <form method="POST" action="?/remove">
                <button on:click={confirmRemove} type="submit" class="btn variant-ghost-error">Remove</button>
            </form>        
		</div>
	</div>
	<div class="card overflow-hidden">
		<div class="p-4 space-y-4">
            <h2 class="h2">Duplicate scenario</h2>
            <form method="POST" action="?/duplicate">
                <div class="space-y-4">
                <label>
                    Slug
                    <input
                        class="input"
                        type="text"
                        value={`${scenario}-copy`}
                        required
                        name="slug"
                    />
                </label>
                <label>
                    Title
                    <input
                        class="input"
                        type="text"
                        required
                        value={`${title} (Copy)`}
                        name="name"
                    />
                </label>
        
                <button on:click={confirmDuplicate} type="submit" class="btn variant-filled-primary">Duplicate</button>
                </div>
            </form>


        </div>
	</div>
</div>
