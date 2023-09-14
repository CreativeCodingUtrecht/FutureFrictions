import { json } from '@sveltejs/kit';
import type { RequestHandler } from './$types';
import scenarios from '$lib/scenarios';

export const GET: RequestHandler = ({ url }) => {
	return json(scenarios.list());
}
