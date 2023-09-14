import { json } from '@sveltejs/kit';
import type { RequestHandler } from './$types';
import fs from 'fs'

export const GET: RequestHandler = ({ params, url }) => {
    const scenario = params.scenario;

    const data = fs.readFileSync(`../data/${scenario}/scenario.json`, 'utf8');

    const contents = JSON.parse(data);

	return json(contents);
}
