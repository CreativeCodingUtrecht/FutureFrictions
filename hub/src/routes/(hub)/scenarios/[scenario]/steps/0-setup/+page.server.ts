import type { Actions, PageServerLoad } from './$types';
import scenarios from '$lib/scenarios';
import { redirect } from '@sveltejs/kit';

export const load: PageServerLoad = ({ params }) => {

    const scenario = params.scenario;
    const json = scenarios.json(scenario);
    const images = scenarios.images(scenario);

    return {
        scenario,
        json,
        images
    }
};

export const actions = {
    save: async ({params, request}) => {
        return save(params, request);
    },
    next: async ({params, request}) => {
        save(params, request);
        redirect(303, `/scenarios/${params.scenario}/steps/1-situation`);
    },
    previous: async ({params, request}) => {
        save(params, request);
        redirect(303, `/scenarios/${params.scenario}`);
    },
} satisfies Actions;

const save = async (params, request) => {
    const scenario = params.scenario;
    const json = scenarios.json(scenario);

    const data = await request.formData();
    const title = data.get('title')
    const author = data.get('author')

    json.name = title;
    json.author = author;
    scenarios.save(scenario, json);
}
