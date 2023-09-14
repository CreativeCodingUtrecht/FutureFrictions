<script lang="ts">
    import type { PageData } from './$types';
    const FF_APP = "http://localhost/ffapp"
    import { goto } from '$app/navigation';

    export let data: PageData;
    
    const scenario = data.scenario;
    const json = data.json;

    const title = json.friction.description;
    const background = json.scene.background;

    const background_url = `/api/scenarios/${scenario}/${background}`;
    const start = () => {
        const url = `${FF_APP}?scenario=${scenario}`
        console.log("Start Unity app: " )

        window.open(url, "ff_app");
    }

    const edit = () => {        
        goto(`/scenarios/${scenario}/edit`);
    }
</script>

<h1>{title}</h1>

<button on:click={start}>Start</button>

<button on:click={edit}>Edit</button>

<img src="{background_url}" />

