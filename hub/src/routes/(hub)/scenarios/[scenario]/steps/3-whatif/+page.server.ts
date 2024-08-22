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
        redirect(303, `/scenarios/${params.scenario}/steps/4-future`);
    },
    previous: async ({params, request}) => {
        save(params, request);
        redirect(303, `/scenarios/${params.scenario}/steps/2-present`);
    },
} satisfies Actions;

const save = async (params, request) => {
    console.log("Saving step 3")
    const scenario = params.scenario;
    const json = scenarios.json(scenario);

    const data = await request.formData();

    if (!json.whatif) {
        json.whatif = {};
    }

    const whatif = data.get('whatif')  
    json.whatif.question = whatif;

    scenarios.save(scenario, json);
}
