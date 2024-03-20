import type { Actions, PageServerLoad } from './$types';
import scenarios from '$lib/scenarios';
import { fail, error } from '@sveltejs/kit';
import fs from 'fs';
import { extname } from 'path';

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
        console.log("Saving JSON")
        const scenario = params.scenario;
        const data = await request.formData();
        const json = data.get('json');
        // console.log("json", json);
        const obj = JSON.parse(json);
        // console.log("json obj", obj);
        scenarios.save(scenario, obj);
    },
    upload: async ({params, request}) => {
        console.log("Saving image")
        const scenario = params.scenario;
        const data = await request.formData();

        const uploadedFile = data?.get('file');

        const filename = `${scenarios.SCENARIOROOT}/${scenario}/${uploadedFile?.name}`;
          
        console.log("filename", filename);

        const buffer = Buffer.from(await uploadedFile?.arrayBuffer());

        scenarios.addImage(scenario, filename, buffer);

          return {
            success: true
          }
    },
    removeImage: async({params, request}) => {
        console.log("Removing image")
        const scenario = params.scenario;
        const data = await request.formData();
        const image = data?.get('image');

        const filename = `../data/${scenario}/${image}`;

        scenarios.removeImage(scenario,image);
    }

} satisfies Actions;