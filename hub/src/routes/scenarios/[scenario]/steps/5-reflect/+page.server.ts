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
    complete: async ({params, request}) => {
        save(params, request);
        redirect(303, `/scenarios/${params.scenario}`);
    },
    previous: async ({params, request}) => {
        save(params, request);
        redirect(303, `/scenarios/${params.scenario}/steps/4-future`);
    },
} satisfies Actions;

const save = async (params, request) => {
    console.log("Saving step 0")
    const scenario = params.scenario;
    const json = scenarios.json(scenario);

    const data = await request.formData();

    scenarios.save(scenario, json);
}
