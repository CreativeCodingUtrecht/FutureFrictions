import type { PageServerLoad } from './$types';
import scenarios from '$lib/scenarios';

export const load: PageServerLoad = ({ params }) => {

    const scenario = params.scenario;
    const json = scenarios.json(scenario);

    return {
        scenario,
        json
    }
};