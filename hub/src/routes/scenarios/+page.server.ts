import type { PageServerLoad } from './$types';
import scenarios from '$lib/scenarios';

export const load: PageServerLoad = () => {
    return {
        scenarios : scenarios.list()
    };
};