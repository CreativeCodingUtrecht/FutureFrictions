import { json } from '@sveltejs/kit';
import { error } from '@sveltejs/kit';
import type { RequestHandler } from './$types';
import fs from 'fs'

export const GET: RequestHandler = ({ params }) => {
    const scenario = params.scenario;

    const filename = `../data/${scenario}/scenario.json`;

    if (!fs.existsSync(filename)) {
        console.log("File does not exist")
        throw error(404, {
            message: 'Requested scenario JSON does not exist',
        });
    }

    const data = fs.readFileSync(filename, 'utf8');

    const contents = JSON.parse(data);

	return json(contents);
}
