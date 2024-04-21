<script lang="ts">
    import type { PageData } from './$types';
    
    export let data: PageData;
    
    const scenario = data.scenario;
    const json = data.json;

    const title = json.friction.description;

</script>

<h4 class="h4"><a href="/scenarios">Scenarios</a> ➡ <a href="/scenarios/{scenario}">{title}</a></h4>

<slot />