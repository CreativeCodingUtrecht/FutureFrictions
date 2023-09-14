<script lang="ts">
    import type { PageData } from './$types';
    const FF_APP = "http://localhost/ffapp"

    export let data: PageData;
    
    const scenario = data.scenario;
    let json = data.json;

    const title = json.friction.description;
    const background = json.scene.background;

    const background_url = `/api/scenarios/${scenario}/${background}`;
    const start = () => {
        const url = `${FF_APP}?scenario=${scenario}`
        console.log("Start Unity app: " )

        window.open(url, "ff_app");
    }

    let raw = JSON.stringify(data.json);
</script>

<h1>Edit: {title}</h1>
<button on:click={start}>Start</button>

<form method="POST">
    <textarea bind:value={raw}></textarea>
    <button>Save</button>
</form>

<textarea></textarea>
