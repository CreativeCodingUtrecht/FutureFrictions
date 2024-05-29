import type { RequestHandler } from './$types';
import scenarios from '$lib/scenarios';

export const GET: RequestHandler = ({ params }) => {
    const scenario = params.scenario;
    const image = params.image;

    const { data, mime } = scenarios.getCharacterImage(scenario,image)

	return new Response(data, {
        headers: {
            "Content-Type": mime
        }
    });

}
