<script lang="ts">
    import type { PageData } from './$types';

    const FF_APP = "http://localhost/ffapp"

    export let data: PageData;
    
    const scenario = data.scenario;
    let json = data.json;
    const images = data.images;

    const title = json.friction.description;
    const background = json.scene.background;

    const background_url = `/api/scenarios/${scenario}/${background}`;
    const url = `${FF_APP}?scenario=${scenario}`

    let raw = JSON.stringify(data.json, null, 4);

    const image_url = `/api/scenarios/${scenario}/`;
</script>

<h1>Scenario | Edit | {title}</h1>
<a href="{url}" target="ff_app" class="btn">Start</a>

<div class="container">
    <div class="pt-5">
        <h2>Scenario JSON</h2>
        <form method="POST" action="?/save"> 
            <textarea name="json" rows="10" cols="110" class="textarea textarea-bordered textarea-lg" bind:value={raw}></textarea>
            <p>
            <button class="btn btn-primary">Save</button>
            </p>
        </form>
    </div>
</div>

<div class="container pt-5">
    <h2>Scenario images</h2>    
    <div class="columns-2">
        {#each data.images as image}
        {#if (image !=null)}
        <div class="card lg:card-side card-bordered">
            <figure>
              <img src="{image_url}{image}" style="max-height:200px">
            </figure> 
            <div class="card-body">
              <h2 class="card-title">{image}</h2> 
              <!-- <p>Rerum reiciendis beatae tenetur excepturi aut pariatur est eos. Sit sit necessitatibus veritatis sed molestiae voluptates incidunt iure sapiente.</p> 
              <div class="card-actions">
                <button class="btn btn-primary">Get Started</button> 
                <button class="btn btn-ghost">More info</button>
              </div> -->
            </div>
          </div> 
          {/if}
        {/each}
    </div>
    
    <form method="POST" action="?/upload" enctype="multipart/form-data"> 
        <input
            class="input input-lg"
            type="file"
            name="file"
            required accept=".jpg, .jpeg, .png"/>        
        <p>
        <button class="btn btn-primary">Upload</button>
        </p>
    </form>


</div>



<style>
    textarea {
        font-family:Consolas,Monaco,Lucida Console,Liberation Mono,DejaVu Sans Mono,Bitstream Vera Sans Mono,Courier New, monospace;
    }
</style>