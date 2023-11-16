<script lang="ts">
    import type { PageData } from './$types';
    import { FF_WEBGL_URL } from '$lib/constants';
    
    export let data: PageData;
    
    const scenario = data.scenario;
    const json = data.json;

    const title = json.friction.description;
    const welcome = json.scene.content.welcome;
    const background = json.scene.background;
    const avatar = json.scene.avatar;

    const background_url = `/api/scenarios/${scenario}/${background}`;
    const avatar_url = `/api/scenarios/${scenario}/${avatar}`;
    const url = `${FF_WEBGL_URL}?scenario=${scenario}`

    const confirmRemove = (e) => {
		if(!confirm("Are you sure you want to delete this scenario? 🤔")) {
            e.preventDefault();
        }
    }

    const confirmDuplicate = (e) => {
		if(!confirm("Are you sure you want to duplicate this scenario? 🤔")) {
            e.preventDefault();
        }
    }

</script>

<h1>{title}</h1>

<div class="container pt-5">
    <ul class="menu px-3 border bg-base-100 menu-horizontal rounded-box">
        <!-- <li class="menu-title">{title}</li> -->
        <li class="inline-block font-bold border-b-2 text-base-content border-primary"><a href="/scenarios/{scenario}" target="FF_WEBGL_URL">View</a></li>
        <li class=""><a href="{url}" target="FF_WEBGL_URL">Play</a></li>        
        <li class=""><a href="/scenarios/{scenario}/edit">Edit</a></li>
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

<br />
<div class="container pt-5">    
  <h2>Duplicate scenario</h2>    
  <form method="POST" action="?/duplicate"> 
    <label>
        Slug
        <input
          class="input input-bordered px-0"
          type="text"
          value={`${scenario}-copy`}
          required
          name="slug"/>        
        </label>          
        <label>
          Display name
          <input
            class="input input-bordered px-0"
            type="text"
            required
            value={`${title} (Copy)`}
            name="name"/>        
          </label>          
  
        <button  on:click={confirmDuplicate} type="submit" class="btn btn-primary">Create</button>
  </form>
</div>

<br />
<div class="container pt-5">    
  <h2>Delete scenario</h2>    
  <form method="POST" action="?/remove"> 
        <button on:click={confirmRemove} type="submit" class="btn btn-outline btn-error">Remove</button>
  </form>
</div>

