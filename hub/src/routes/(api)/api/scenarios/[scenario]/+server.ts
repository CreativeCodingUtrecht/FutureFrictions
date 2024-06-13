import { json } from '@sveltejs/kit';
import { error } from '@sveltejs/kit';
import type { RequestHandler } from './$types';
import scenarios from '$lib/scenarios'

export const GET: RequestHandler = ({ params }) => {
    const scenario = params.scenario;

	return json(scenarios.json(scenario));
}
