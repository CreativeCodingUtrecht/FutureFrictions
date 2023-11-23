import fs from 'fs';
import { error } from '@sveltejs/kit';
import { env } from '$env/dynamic/private'

export const DATAROOT = env.DATAROOT || '../data';
export const SCENARIOROOT = `${DATAROOT}/scenarios`;
export const TEMPLATEROOT = `${DATAROOT}/templates`;

const list = () => {
	const slugs = fs
		.readdirSync(SCENARIOROOT, { withFileTypes: true })
		.filter((dirent) => dirent.isDirectory())
		.map((dirent) => dirent.name);
		
	let scenarios = {}
	for (let slug of slugs) {
		const data = json(slug);
		scenarios[slug] = data;
	}

	return scenarios;
};

const json = (scenario: String) => {
	const filename = `${SCENARIOROOT}/${scenario}/scenario.json`;

	if (!fs.existsSync(filename)) {
		console.log('File does not exist');
		throw error(404, {
			message: 'Requested scenario JSON does not exist'
		});
	}

	const data = fs.readFileSync(filename, 'utf8');

	return JSON.parse(data);
};

const images = (scenario: String) => {
	const images = fs
		.readdirSync(`${SCENARIOROOT}/${scenario}/`, { withFileTypes: true })
		.filter(
			(dirent) =>
				dirent.isFile() &&
				(dirent.name.endsWith('.png') ||
					dirent.name.endsWith('.jpg') ||
					dirent.name.endsWith('.jpeg'))
		)
		.map((dirent) => dirent.name);

	// console.log(images)

	return images;
};

// const usedImages = (json: any) => {
// 	const images = [
// 		json.scene.background,
// 		json.scene.avatar,
// 		json.friction.avatar,
// 		json.friction.options.a.avatar,
// 		...json.friction.options.a.sprites.foreground,
// 		json.friction.options.b.avatar,
// 		...json.friction.options.b.sprites.background,
// 		json.friction.options.c.avatar,
// 		...json.friction.options.c.sprites.floating,
// 		json.actors[0].sprite,
// 		json.actors[0].avatar,
// 		json.actors[1].sprite,
// 		json.actors[1].avatar,
// 		json.actors[2].sprite,
// 		json.actors[2].avatar
// 	];

// 	return images;
// };

const save = (scenario: String, json: any) => {
	const filename = `${SCENARIOROOT}/${scenario}/scenario.json`;

	if (!fs.existsSync(filename)) {
		console.log('File does not exist');
		throw error(404, {
			message: 'Requested scenario JSON does not exist'
		});
	}

	fs.writeFileSync(filename, JSON.stringify(json, null, 4));
};

const duplicate = (scenario: String, newScenario: String, name: String) => {
	if (scenario === newScenario) return;

	const dir = `${SCENARIOROOT}/${scenario}`;
	const newDir = `${SCENARIOROOT}/${newScenario}`;

	// Create directory
	if (!fs.existsSync(newDir)){
		fs.mkdirSync(newDir);
	}

	// Copy contents
	fs.cpSync(dir, newDir, { recursive: true });

	// Update JSON
	const data = json(newScenario)
	data.friction.description = name;
	save(newScenario,data);
}

const create = (newScenario: String, name: String) => {
	const dir = `${TEMPLATEROOT}/new`;
	const newDir = `${SCENARIOROOT}/${newScenario}`;

	// Create directory
	if (!fs.existsSync(newDir)){
		fs.mkdirSync(newDir);
	}

	// Copy contents
	fs.cpSync(dir, newDir, { recursive: true });

	// Update JSON
	const data = json(newScenario)
	data.friction.description = name;
	save(newScenario,data);
}

const remove = (scenario: String) => {
	const dir = `${SCENARIOROOT}/${scenario}`;

	// Check directory
	if (!fs.existsSync(dir)){
		console.log('File does not exist');
		throw error(404, {
			message: 'Requested scenario does not exist'
		});

	}

	// Copy contents
	fs.rmSync(dir, { recursive: true, force: true });
}

const addImage = (scenario: String, filename: any, data: Buffer) => {
	try {
		fs.writeFileSync(filename, data);
	} catch {
		throw error(500, 'Unable to save image to disk');
	}
};

const getImage = (scenario: String, image: any) => {
	const filename = `${SCENARIOROOT}/${scenario}/${image}`;
    const extension = filename.split('.').pop();
    const mime = `image/${extension}`;

    if (!fs.existsSync(filename)) {
        console.log("File does not exist")
        throw error(404, {
            message: 'Requested image does not exist',
        });
    }

    const data = fs.readFileSync(filename);

	return {
		data,
		mime
	}
}

const removeImage = (scenario: String, image: any) => {
	const filename = `${SCENARIOROOT}/${scenario}/${image}`;

    if (!fs.existsSync(filename)) {
        console.log("File does not exist")
        throw error(404, {
            message: 'Requested image does not exist',
        });
    }

	fs.rmSync(filename);
}

export default {
	list,
	json,
	save,
	images,
	// usedImages,
    addImage,
	getImage,
	removeImage,
	duplicate,
	remove,
	create
};
