<script lang="ts">
    import type { PageData } from './$types';
	import { FF_WEBGL_URL } from '$lib/constants';

    export let data: PageData;
    
    const scenario = data.scenario;
    const json = data.json;

    const title = json.friction.description;
	const url = `${FF_WEBGL_URL}?scenario=${scenario}`;

</script>

<h4 class="h4">
    <a href="/scenarios">Scenarios</a> ➡ <a href="/scenarios/{scenario}">{title}</a>
    &nbsp;<a href={url} class="text-surface-500" target="FF_WEBGL_URL">▶</a>
</h4>

<slot />