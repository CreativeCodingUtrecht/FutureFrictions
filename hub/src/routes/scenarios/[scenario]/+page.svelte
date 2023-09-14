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
    const url = `${FF_APP}?scenario=${scenario}`
</script>

<h1>Scenario | {title}</h1>

<a href="{url}" target="ff_app" class="btn btn-primary">Start</a>

<a href="/scenarios/{scenario}/edit" class="btn btn-primary">Edit</a>

<img src="{background_url}" />

