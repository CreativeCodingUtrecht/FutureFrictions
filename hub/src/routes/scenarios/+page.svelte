<script lang="ts">
    import type { PageData } from './$types';
    import { goto } from '$app/navigation';
    
    export let data: PageData;
</script>

<div class="prose">
    <h1>Scenarios</h1>

    <div class="grid grid-cols-4 gap-4">
    {#each data.scenarios as scenario}
    <div class="card bg-base-200 shadow-xl">
        <div class="card-body">
            <h2 class="card-title">{scenario}</h2>
            <!-- <p>If a dog chews shoes whose shoes does he choose?</p> -->
            <div class="card-actions justify-end">
                <a href="/scenarios/{scenario}" class="btn btn-primary">Open</a>
            </div>
        </div>
    </div>
    {/each}
    </div>
</div>
