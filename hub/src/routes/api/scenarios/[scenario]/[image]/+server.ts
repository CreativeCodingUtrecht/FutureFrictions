import type { RequestHandler } from './$types';
import fs from 'fs'

export const GET: RequestHandler = ({ params }) => {
    const scenario = params.scenario;
    const image = params.image;

    const data = fs.readFileSync(`../data/${scenario}/${image}`);

	return new Response(data, {
        headers: {
            "Content-Type": "image/png"
        }
    });

}
