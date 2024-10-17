import type { RequestHandler } from './$types';
import scenarios from '$lib/scenarios';
import JSZip from 'jszip';

export const GET: RequestHandler = ({ params }) => {
    const scenario = params.scenario;

    const { data, mime } = scenarios.getImage(scenario,image)

    const zip = new JSZip();

	return new Response(data, {
        headers: {
            "Content-Type": mime
        }
    });

}
