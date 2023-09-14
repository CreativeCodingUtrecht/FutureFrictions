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
    save: async ({params, request}) => {
        // JSON
        console.log("Saving JSON")
        const scenario = params.scenario;
        const data = await request.formData();
        const json = data.get('json');
        console.log("json", json);
        const obj = JSON.parse(json);
        console.log("json obj", obj);

        scenarios.save(scenario, obj);
    },
} satisfies Actions;