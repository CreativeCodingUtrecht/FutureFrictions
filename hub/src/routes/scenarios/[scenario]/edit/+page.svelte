<script lang="ts">
    import type { PageData } from './$types';
    import { FF_WEBGL_URL } from '$lib/constants';

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
    const url = `${FF_WEBGL_URL}?scenario=${scenario}`

    let raw = JSON.stringify(data.json, null, 4);

    const image_url = `/api/scenarios/${scenario}/`;

    const confirmRemove = (e) => {
		if(!confirm("Are you sure you want to remove this image? 🤔")) {
            e.preventDefault();
        }
    }
</script>

<h1>{title}</h1>

<div class="container pt-5">
    <ul class="menu px-3 border bg-base-100 menu-horizontal rounded-box">
        <!-- <li class="menu-title">{title}</li> -->
        <li class=""><a href="/scenarios/{scenario}" target="FF_WEBGL_URL">View</a></li>
        <li class="inline-block font-bold border-b-2 text-base-content border-primary"><a href="/scenarios/{scenario}/edit">Edit</a></li>        
        <li class=""><a href="{url}" target="FF_WEBGL_URL">Play</a></li>
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
                <a href="{url}" class="btn btn-primary" target="FF_WEBGL_URL">Play</a>
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
              <form method="POST" action="?/removeImage"> 
                <input type="hidden" name="image" value={image} />
                <button on:click={confirmRemove} type="submit" class="btn btn-outline btn-xs btn-error"><svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" /></svg></button>
                </form>            
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
