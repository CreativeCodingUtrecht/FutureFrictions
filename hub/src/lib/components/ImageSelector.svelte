<script lang="ts">

    export let images = [];
    export let values = {};
    export let field = "avatar"
    export let scenario;
    export let input_file = "file";
    export let input_value = "avatar";
    export let extraClass = "max-w-80";

	let avatarImage: String;

	const updateImage = (e) => {
		const [file] = e.target.files;
		if (file) {
			avatarImage = URL.createObjectURL(file)
			values[field] = file.name;
		}
	}

	const updateImageFromSelect = (e) => {
		const value = e.target.value;
		console.log(`${value}`);
		if (value !== "none") {
			values[field] = e.target.value;
		}
	}

	const updateImageDirect = (e) => {
		console.log(`${e.target.value}`);
		values[field] = e.target.value;
	}    
</script>

<div class="card p-4 {extraClass}">
    <img id="preview" src={avatarImage ? `${avatarImage}` : `/api/scenarios/${scenario}/${values[field]}`} alt={values[field]} />
    
    <input id="imgInput" type="file" name={input_file} accept=".jpg, .jpeg, .png" on:click={(e) => {e.stopPropagation();}} on:change={updateImage} />

    <p>Or paste the file name of the image</p>

    <input
        id="imageText"
        value={values[field]}
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