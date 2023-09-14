<script lang="ts">
    import type { PageData } from './$types';
    import { goto } from '$app/navigation';
    
    export let data: PageData;

    const browse = () => {
        goto('/scenarios');
    }
</script>

<div class="hero">
    <div class="hero-content text-center">
        <div class="">
            <h1>Welcome to the Future Frictions Hub</h1>
            <div class="pt-5">
                <button class="btn btn-primary" on:click={browse}>Browse scenarios</button>
            </div>
        </div>
    </div>
</div>