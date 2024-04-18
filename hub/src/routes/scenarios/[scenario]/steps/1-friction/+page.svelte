<script lang="ts">
	import type { PageData, ActionData } from './$types';

	export let form: ActionData;
	export let data: PageData;

	const values = {
		statement: form?.statement || data?.json.scene.content.welcome || '',
		avatar : form?.avatar || data?.json.scene.avatar || ""
	};

	let avatarImage: String;

	const updateImage = (e) => {
		const [file] = e.target.files;
		if (file) {
			avatarImage = URL.createObjectURL(file)
			values.avatar = file.name;
		}
	}

	const updateImageDirect = (e) => {
		console.log(`${e.target.value}`);
		values.avatar = e.target.value;
	}
</script>

<div>
	<form method="POST" action="?/save"  enctype="multipart/form-data">
		<div class="space-y-4">
			<label class="label space-y-4">
				<p>
					<span>
						Fill out this text box with a 'frictional statement' you would like to explore further.
						Feel free to choose one from this list, or come up with yours.
					</span>
				</p>

				<p>
					<span>
						<i>A frictional statement refers to an existing trend, change or vision that is potentially controversial and leads to dilemmas.</i>
					</span>
				</p>

				<textarea
					value={values.statement}
					class="textarea"
					title="Frictional statement"
					rows="3"
					name="statement"
				/>
			</label>

			<p>
				Examples of frictional statements: 
			</p>				
			<ul class="list">
				<li><span>🌿</span><span class="flex-auto">Implementing high-taxes on single-use plastics to finance urban green spaces</span></li>
				<li><span>💰</span><span class="flex-auto">Implementing a universal basic income to counter automation-induced unemployment</span></li>
				<li><span>🧹</span><span class="flex-auto">Using AI technology to reduce street litter</span></li>
				<li><span>🚨</span><span class="flex-auto">Using sensors to control undesirable behaviors in the city</span></li>					
			</ul>

			<br />

			<label class="label space-y-4">
				<span>What character introduces this frictional statement?</span>
	
				<div class="card p-4 max-w-80">
					<img id="preview" src={avatarImage ? `${avatarImage}` : `/api/scenarios/${data?.scenario}/${values.avatar}`} alt={values.avatar} />
					
					<input id="imgInput" type="file" name="file" accept=".jpg, .jpeg, .png" on:click={(e) => {e.stopPropagation();}} on:change={updateImage} />

					<p>Or paste the file name of the image</p>

					<input
						id="imageText"
						value={values.avatar}
						class="input"
						title="Avatar"
						type="text"
						name="avatar"
						on:change={updateImageDirect}
					/>

					<a type="button" class="btn variant-filled mt-4" href={`/scenarios/${data?.scenario}/images`}>See Image Gallery</a>
					<!-- <button type="button" class="btn variant-filled" on:click={() => { console.log("Clear input value") }}>clear</button> -->
				</div>
			</label>
	
		</div>

		<br />
		<button class="btn variant-filled-primary">Save</button>
		&nbsp;&nbsp;&nbsp;&nbsp; 
		<button class="btn variant-ghost-primary" formaction="?/previous">Back</button>		
		<button class="btn variant-ghost-primary" formaction="?/next">Next</button>
	</form>
</div>

<style>
	input {
		width: 100%;
	}
</style>