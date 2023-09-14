<script lang="ts">
    import type { PageData } from './$types';
    import { FF_WEBGL } from '$lib/constants';

    export let data: PageData;
    
    const scenario = data.scenario;
    let json = data.json;

    const images = data.images;

    const title = json.friction.description;
    const welcome = json.scene.content.welcome;
    const background = json.scene.background;
    const avatar = json.scene.avatar;

    const background_url = `/api/scenarios/${scenario}/${background}`;
    const avatar_url = `/api/scenarios/${scenario}/${avatar}`;
    const url = `${FF_WEBGL}?scenario=${scenario}`

    let raw = JSON.stringify(data.json, null, 4);

    const image_url = `/api/scenarios/${scenario}/`;
</script>

<h1>{title}</h1>

<div class="container pt-5">
    <ul class="menu px-3 border bg-base-100 menu-horizontal rounded-box">
        <!-- <li class="menu-title">{title}</li> -->
        <li class=""><a href="/scenarios/{scenario}" target="ff_webgl">View</a></li>
        <li class="inline-block font-bold border-b-2 text-base-content border-primary"><a href="/scenarios/{scenario}/edit">Edit</a></li>        
        <li class=""><a href="{url}" target="ff_webgl">Play</a></li>
    </ul>
</div>

<div class="container pt-5">
    <div class="flex items-center w-full px-4 py-10 bg-cover card bg-base-200" style="background-image: url(&quot;{background_url}&quot;);">
        <div class="card glass lg:card-side text-neutral-content">
          <figure class="p-6">
            <img src="{avatar_url}" class="rounded-lg shadow-lg" style="max-width:200px">
          </figure> 
          <div class="max-w-md card-body">
            <h2 class="card-title">{title}</h2> 
            <p>{welcome}</p> 
            <div class="card-actions">
                <a href="{url}" class="btn btn-primary" target="ff_webgl">Play</a>
            </div>
          </div>
        </div>
      </div>
</div>

<div class="container">
    <div class="pt-5">
        <h2>Scenario definition</h2>
        <form method="POST" action="?/save"> 
            <textarea 
                name="json" 
                spellcheck={false}
                rows="10" 
                cols="110" 
                class="text-sm font-mono textarea textarea-bordered font-mono textarea-lg"                 
                bind:value={raw}></textarea>
            <p>
            <button class="btn btn-primary">Save</button>
            </p>
        </form>
    </div>
</div>

<div class="container pt-5">
    <h2>Image library</h2>    
    <div class="grid grid-cols-4 gap-4">
        {#each images as image}
        {#if (image !=null)}
        <div class="card lg:card-side card-bordered bg-base-200">
            <figure>
              <img src="{image_url}{image}" style="max-width:100px">
            </figure> 
            <div class="card-body">
              <p class="card-title text-xs">{image}</p> 
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
</div>

<div class="container pt-5">    
    <h2>Upload new image</h2>    
    <form method="POST" action="?/upload" enctype="multipart/form-data"> 
        <input
            class="input input-lg px-0"
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
        width:100%;
        white-space: pre;
        overflow-wrap: normal;
        overflow-x: scroll;        
    }    
</style>
