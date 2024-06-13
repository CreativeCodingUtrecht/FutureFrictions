<script lang="ts">
    import type { PageData } from './$types';
	import { FF_WEBGL_URL } from '$lib/constants';
    import Play from '$lib/components/Play.svelte';

    export let data: PageData;
    
    const scenario = data.scenario;
    const json = data.json;

    const title = json.name;
	const url = `${FF_WEBGL_URL}?scenario=${scenario}`;

</script>

<h4 class="h4">
    <a href="/">Scenarios</a> ➡ <a href="/scenarios/{scenario}">{title}</a>
    &nbsp;<Play {scenario} button={false} />
</h4>

<slot />