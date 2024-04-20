<script lang="ts">

    export let title : string = "Create a collage"
    export let images : string[] = [];
    export let values = {};
    export let field : string = "avatar"
    export let input : string;
    export let scenario : string;
    export let upload : string = "file";
    export let extraClass : string = "max-w-80";
	let previewImage: string;

	const updateImage = (e) => {
		const [file] = e.target.files;
		if (file) {
			previewImage = URL.createObjectURL(file)
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

<div class="card bg-surface-200/30 p-4 space-y-4 {extraClass}">
    <!-- <h4 class="h4">{title}</h4> -->
    <img id="preview" src={previewImage ? `${previewImage}` : `/api/scenarios/${scenario}/${values[field]}`} alt={values[field]} />
    
    <input id="imgInput" class="input" type="file" name={upload} accept=".jpg, .jpeg, .png" on:click={(e) => {e.stopPropagation();}} on:change={updateImage} />

    <p>Or, Select an image from the library</p>
    
    <select class="select" on:change={updateImageFromSelect}>
        <option disabled selected value>No image selected.</option>
        {#each images as image}
            <option value={image}>{image}</option>
        {/each}
    </select>

    <input
        id="imageText"
        value={values[field]}
        class="input"
        type="hidden"
        name={input ? input : field}
        on:change={updateImageDirect}
    />

    <a type="button" target="_images" class="btn variant-ghost-primary mt-4" href={`/scenarios/${scenario}/images`}>See Image Gallery</a>
</div>

<style>
	input {
		width: 100%;
	}
</style>