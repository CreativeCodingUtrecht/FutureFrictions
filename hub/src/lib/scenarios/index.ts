import fs from 'fs';
import { error } from '@sveltejs/kit';
import { DATAROOT } from '$lib/constants';

const list = () => {
	const scenarios = fs
		.readdirSync(DATAROOT, { withFileTypes: true })
		.filter((dirent) => dirent.isDirectory())
		.map((dirent) => dirent.name);

	return scenarios;
};

const json = (scenario: String) => {
	const filename = `${DATAROOT}/${scenario}/scenario.json`;

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
		.readdirSync(`${DATAROOT}/${scenario}/`, { withFileTypes: true })
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
	const filename = `${DATAROOT}/${scenario}/scenario.json`;

	if (!fs.existsSync(filename)) {
		console.log('File does not exist');
		throw error(404, {
			message: 'Requested scenario JSON does not exist'
		});
	}

	fs.writeFileSync(filename, JSON.stringify(json, null, 4));
};

const addImage = (scenario: String, filename: any, data: Buffer) => {
	try {
		fs.writeFileSync(filename, data);
	} catch {
		throw error(500, 'Unable to save image to disk');
	}
};

export default {
	list,
	json,
	save,
	images,
	// usedImages,
    addImage
};
