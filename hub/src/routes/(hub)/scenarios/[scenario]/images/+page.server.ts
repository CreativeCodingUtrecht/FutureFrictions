import type { Actions, PageServerLoad } from './$types';
import scenarios from '$lib/scenarios';

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