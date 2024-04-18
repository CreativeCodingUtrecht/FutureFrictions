<script lang="ts">

    export let images = [];
    export let values = {};
    export let scenario;
    export let input_file = "file";
    export let input_value = "avatar";

	let avatarImage: String;

	const updateImage = (e) => {
		const [file] = e.target.files;
		if (file) {
			avatarImage = URL.createObjectURL(file)
			values.avatar = file.name;
		}
	}

	const updateImageFromSelect = (e) => {
		const value = e.target.value;
		console.log(`${value}`);
		if (value !== "none") {
			values.avatar = e.target.value;
		}
	}

	const updateImageDirect = (e) => {
		console.log(`${e.target.value}`);
		values.avatar = e.target.value;
	}    
</script>

<div class="card p-4 max-w-80">
    <img id="preview" src={avatarImage ? `${avatarImage}` : `/api/scenarios/${scenario}/${values.avatar}`} alt={values.avatar} />
    
    <input id="imgInput" type="file" name={input_file} accept=".jpg, .jpeg, .png" on:click={(e) => {e.stopPropagation();}} on:change={updateImage} />

    <p>Or paste the file name of the image</p>

    <input
        id="imageText"
        value={values.avatar}
        class="input"
        title="Avatar"
        type="hidden"
        name={input_value}
        on:change={updateImageDirect}
    />
    
    <select class="select" on:change={updateImageFromSelect}>
        <option></option>
        {#each images as image}
            <option value={image}>{image}</option>
        {/each}
    </select>

    <a type="button" class="btn variant-filled mt-4" href={`/scenarios/${scenario}/images`}>See Image Gallery</a>
    <!-- <button type="button" class="btn variant-filled" on:click={() => { console.log("Clear input value") }}>clear</button> -->
</div>

<style>
	input {
		width: 100%;
	}
</style>