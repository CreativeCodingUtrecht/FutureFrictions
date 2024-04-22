import type { PageServerLoad } from './$types';
import scenarios from '$lib/scenarios';
import { redirect } from '@sveltejs/kit';

export const load: PageServerLoad = () => {
    return {
        scenarios : scenarios.list()
    };
};

export const actions = {
    create: async ({params, request}) => {
        console.log("Creating scenario based on template")

        const data = await request.formData();
        const newScenario = data.get('slug');
        const newName = data.get('name');

        console.log("Creating -- New:", newScenario);
        scenarios.create(newScenario, newName);
        throw redirect(303, `/scenarios/${newScenario}/steps/0-setup`);
    }    
} satisfies Actions;