<script lang="ts">
    import type { PageData } from './$types';
    const FF_APP = "http://localhost/ffapp"

    export let data: PageData;
    
    const scenario = data.scenario;
    let json = data.json;

    const title = json.friction.description;
    const background = json.scene.background;

    const background_url = `/api/scenarios/${scenario}/${background}`;
    const url = `${FF_APP}?scenario=${scenario}`

    let raw = JSON.stringify(data.json);
</script>

<h1>Scenario | Edit | {title}</h1>
<a href="{url}" target="ff_app" class="btn btn-primary">Start</a>


<form method="POST">
    <textarea class="textarea textarea-bordered textarea-lg w-full max-w-xs" bind:value={raw}></textarea>
    <button>Save</button>
</form>

<textarea></textarea>
