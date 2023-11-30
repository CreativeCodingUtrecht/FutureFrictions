<script lang="ts">
    import type { PageData } from './$types';
    import { goto } from '$app/navigation';
    
    export let data: PageData;

    const confirmCreate = (e) => {
		if(!confirm("Are you sure you want to create a new scenario? 🤔")) {
            e.preventDefault();
        }
    }

</script>

<div class="prose">
    <h1>Scenarios</h1>

    <div class="grid grid-cols-4 gap-4">
    {#each Object.keys(data.scenarios) as scenario}
    <div class="card bg-base-200 shadow-xl image-full">      
        <figure><img src="{data.scenarios[scenario].scene.background ? `/api/scenarios/${scenario}/${data.scenarios[scenario].scene.background}` : `/placeholders/background.jpg`}" /></figure>      
        <div class="card-body">
            <h2 class="card-title">{data.scenarios[scenario].friction.description}</h2>
            <div class="card-actions justify-end">
                <a href="/scenarios/{scenario}" class="btn btn-primary">Open</a>
            </div>
        </div>
    </div>
    {/each}
    </div>    
</div>

<br />
<div class="container pt-5">    
  <h2>Create new scenario</h2>    
  <form method="POST" action="?/create"> 
    <label>
        Slug
        <input
          class="input input-bordered px-0"
          type="text"
          required
          name="slug"/>        
        </label>          
        <label>
          Name
          <input
            class="input input-bordered px-0"
            type="text"
            required
            name="name"/>        
          </label>          
  
        <button on:click={confirmCreate} type="submit" class="btn btn-primary">Create</button>
  </form>
</div>