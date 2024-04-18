<script lang="ts">
    import type { PageData } from './$types';
    
    export let data: PageData;
    
    const scenario = data.scenario;
    const json = data.json;

    const title = json.friction.description;

</script>

<h1 class="h1"><a href="/scenarios/{scenario}">{title}</a></h1>

<slot />