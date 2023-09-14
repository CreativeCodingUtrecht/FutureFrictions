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

    let raw = JSON.stringify(data.json, null, 4);
</script>

<h1>Scenario | Edit | {title}</h1>
<a href="{url}" target="ff_app" class="btn">Start</a>


<div class="pt-5">
    <form method="POST" action="?/save"> 
        <label>
            Scenario JSON
            <textarea name="json" rows="10" cols="110" class="textarea textarea-bordered textarea-lg" bind:value={raw}></textarea>
        </label>        
        <p>
        <button class="btn btn-primary">Save</button>
        </p>
    </form>
</div>

<textarea></textarea>

<style>
    textarea {
        font-family:Consolas,Monaco,Lucida Console,Liberation Mono,DejaVu Sans Mono,Bitstream Vera Sans Mono,Courier New, monospace;
    }
</style>