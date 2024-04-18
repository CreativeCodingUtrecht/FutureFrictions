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
    console.log("Saving step 0")
    const scenario = params.scenario;
    const json = scenarios.json(scenario);

    const data = await request.formData();

    const statement = data.get('statement')
    json.scene.content.welcome = statement;

    const avatar = data.get('avatar')
    json.scene.avatar = avatar;

    const uploadedFile = data?.get('file');

    if (uploadedFile?.name && uploadedFile.name.length > 0 && uploadedFile?.name !== "undefined" && uploadedFile?.name !== "") {
        const filename = `${scenarios.SCENARIOROOT}/${scenario}/${uploadedFile?.name}`;  
        console.log("filename", filename);
        const buffer = Buffer.from(await uploadedFile?.arrayBuffer());
        scenarios.addImage(scenario, filename, buffer);
    }

    scenarios.save(scenario, json);
}
