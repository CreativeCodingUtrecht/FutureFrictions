<script lang="ts">
    import type { PageData } from './$types';
    
    export let data: PageData;
</script>

<h1>Scenarios</h1>

<ul>
    {#each data.scenarios as scenario}
    <li><a href="scenarios/{scenario}">{scenario}</a></li>
    {/each}
</ul>
