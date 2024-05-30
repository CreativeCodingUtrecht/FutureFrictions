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
        redirect(303, `/scenarios/${params.scenario}/steps/2-present`);
    },
    previous: async ({params, request}) => {
        save(params, request);
        redirect(303, `/scenarios/${params.scenario}/steps/0-setup`);
    },
} satisfies Actions;

const save = async (params, request) => {
    console.log("Saving step 1")
    const scenario = params.scenario;
    const json = scenarios.json(scenario);

    const data = await request.formData();

    if (!json.friction) {
        json.friction = {};
    }

    const statement = data.get('statement')
    json.friction.frictionalstatement = statement;

    const avatar = data.get('avatar')
    json.friction.avatar = avatar;

    const avatarFile = data?.get('file');

    if (avatarFile?.name && avatarFile.name.length > 0 && avatarFile?.name !== "undefined") {
        const filename = `${scenarios.SCENARIOROOT}/${scenario}/${avatarFile?.name}`;  
        console.log("filename", filename);
        const buffer = Buffer.from(await avatarFile?.arrayBuffer());
        scenarios.addImage(scenario, filename, buffer);
    }

    scenarios.save(scenario, json);
}
