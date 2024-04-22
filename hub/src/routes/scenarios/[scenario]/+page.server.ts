import { redirect } from '@sveltejs/kit';
import type { Actions, PageServerLoad } from './$types';
import scenarios from '$lib/scenarios';

export const load: PageServerLoad = ({ params }) => {

    const scenario = params.scenario;
    const json = scenarios.json(scenario);

    return {
        scenario,
        json
    }
};

export const actions = {
    duplicate: async ({params, request}) => {
        console.log("Duplicating scenario")
        const scenario = params.scenario;

        const data = await request.formData();
        const newScenario = data.get('slug');
        const newName = data.get('name');

        console.log("Duplicating -- Old:", scenario, "New:", newScenario);
        scenarios.duplicate(scenario, newScenario, newName);
        throw redirect(303, `/scenarios/${newScenario}/steps/0-setup`);
    },
    remove: async ({params, request}) => {
        const scenario = params.scenario;
        console.log("Removing scenario", scenario)

        scenarios.remove(scenario);
        throw redirect(303, '/scenarios');
    }    
} satisfies Actions;