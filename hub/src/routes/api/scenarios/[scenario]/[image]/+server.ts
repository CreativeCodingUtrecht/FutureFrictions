import type { RequestHandler } from './$types';
import { error } from '@sveltejs/kit';
import fs from 'fs'
import { DATAROOT } from '$lib/constants';

export const GET: RequestHandler = ({ params }) => {
    const scenario = params.scenario;
    const image = params.image;

    const filename = `${DATAROOT}/${scenario}/${image}`;
    const extension = filename.split('.').pop();
    const mime = `image/${extension}`;

    if (!fs.existsSync(filename)) {
        console.log("File does not exist")
        throw error(404, {
            message: 'Requested image does not exist',
        });
    }

    const data = fs.readFileSync(filename);

	return new Response(data, {
        headers: {
            "Content-Type": mime
        }
    });

}
