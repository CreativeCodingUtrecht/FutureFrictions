<script lang="ts">
  	import { TabGroup, TabAnchor } from '@skeletonlabs/skeleton';
  	import { page } from '$app/stores';

    import type { PageData } from './$types';
    import { FF_WEBGL_URL } from '$lib/constants';
    
    export let data: PageData;
    
    const scenario = data.scenario;
    const json = data.json;

    const title = json.friction.description;
    const url = `${FF_WEBGL_URL}?scenario=${scenario}`
</script>

<h1 class="h1">{title}</h1>

<TabGroup>
	<TabAnchor href="/scenarios/{scenario}" selected={$page.url.pathname === `/scenarios/${scenario}`}>View</TabAnchor>
	<TabAnchor href="/scenarios/{scenario}/images" selected={$page.url.pathname === `/scenarios/${scenario}/images`}>Images</TabAnchor>
  <TabAnchor href="/scenarios/{scenario}/edit" selected={$page.url.pathname === `/scenarios/${scenario}/edit`}>Edit</TabAnchor>
  <TabAnchor href={url} target="FF_WEBGL_URL">Play</TabAnchor>
</TabGroup>

<slot />