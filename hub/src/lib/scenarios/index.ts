import fs from 'fs';
import { error } from '@sveltejs/kit';
import { env } from '$env/dynamic/private'
import path from 'path';

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

const images = (scenario: String, subdir : String = "") => {
	const dir = `${SCENARIOROOT}/${scenario}/${subdir}`

	// Check directory
	if (!fs.existsSync(dir)){
		console.log('File does not exist');
		return [];
	}

	return fs
		.readdirSync(dir, { withFileTypes: true })
		.filter(
			(dirent) =>
				dirent.isFile() &&
				(dirent.name.endsWith('.png') ||
					dirent.name.endsWith('.jpg') ||
					dirent.name.endsWith('.jpeg'))
		)
		.map((dirent) => dirent.name);
};

const backgrounds = (scenario: String) => {
	return images(scenario, 'backgrounds');
}

const elements = (scenario: String) => {
	return images(scenario, 'elements');
}

const characters = (scenario: String) => {
	return images(scenario, 'characters');
}

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
	data.name = name;
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
	data.name = name;
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
		const dir = path.dirname(filename);
		console.log("Adding image to directory", dir);
		if (!fs.existsSync(dir)){
			fs.mkdirSync(dir, { recursive: true });
		}
		fs.writeFileSync(filename, data);
	} catch {
		throw error(500, 'Unable to save image to disk');
	}
};

const getImage = (scenario: String, image: any, subdir: String) => {
	const filename = `${SCENARIOROOT}/${scenario}/${subdir ? `${subdir}/` : ''}${image}`;
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

const getBackgroundImage = (scenario: String, image: any) => {
	return getImage(scenario, image, 'backgrounds');
}

const getElementImage = (scenario: String, image: any) => {
	return getImage(scenario, image, 'elements');
}

const getCharacterImage = (scenario: String, image: any) => {
	return getImage(scenario, image, 'characters');
}
	
const getCollageImage = (scenario: String, image: any) => {
	return getImage(scenario, image, 'collage');
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
	backgrounds, 
	elements,
	characters,
	// usedImages,
    addImage,
	getImage,
	getBackgroundImage,
	getElementImage,
	getCollageImage,	
	getCharacterImage,
	removeImage,
	duplicate,
	remove,
	create,
	SCENARIOROOT
};
