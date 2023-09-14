import { json } from '@sveltejs/kit';
import type { RequestHandler } from './$types';
import fs from 'fs'

export const GET: RequestHandler = ({ url }) => {
    const scenarios = fs.readdirSync(`../data`, { withFileTypes: true })
      .filter(dirent => dirent.isDirectory())
      .map(dirent => dirent.name)

	return json(scenarios);
}
