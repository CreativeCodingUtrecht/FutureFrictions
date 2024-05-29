import type { Actions, PageServerLoad } from './$types';
import scenarios from '$lib/scenarios';
import { redirect } from '@sveltejs/kit';

export const load: PageServerLoad = ({ params }) => {

    const scenario = params.scenario;
    const json = scenarios.json(scenario);
    const images = scenarios.images(scenario);

    const backgrounds = scenarios.backgrounds(scenario);
    const elements = scenarios.elements(scenario);
    const characters = scenarios.characters(scenario);

    return {
        scenario,
        json,
        images,
        backgrounds,
        elements,
        characters
    }
};

export const actions = {
    save: async ({params, request}) => {
        return save(params, request);
    },
    next: async ({params, request}) => {
        save(params, request);
        redirect(303, `/scenarios/${params.scenario}/steps/3-whatif`);
    },
    previous: async ({params, request}) => {
        save(params, request);
        redirect(303, `/scenarios/${params.scenario}/steps/1-friction`);
    },
} satisfies Actions;

const saveImage = async (scenario, data, name) => {
    const file = data?.get(name);
    if (file?.name && file.name.length > 0 && file.name !== "undefined" && file.name !== "") {        
        const filename = `${scenarios.SCENARIOROOT}/${scenario}/${file.name}`;  
        console.log("Saving image with filename", filename);        
        const buffer = Buffer.from(await file.arrayBuffer());
        scenarios.addImage(scenario, filename, buffer);
    }    
}

const save = async (params, request) => {
    console.log("Saving step 2")

    const scenario = params.scenario;
    const json = scenarios.json(scenario);
    const data = await request.formData();
        
    // Collage (fabric.js)
    const collagejson = data.get('collage')
    const collage = JSON.parse(collagejson);
    if (!json.collage) {
        json.collage = {}
    }
    if (!json.collage.present) {
        json.collage.present = {}
    }
    json.collage.present.canvas = collage;

    // Definition of the collage used for visualisations
    const definitionjson = data.get('definition');
    const definition = JSON.parse(definitionjson);
    json.collage.present.definition = definition;

    // Emerging friction
    const emergingfriction = data.get('emergingfriction')
    json.scene.content.emergingfriction = emergingfriction;
    
    scenarios.save(scenario, json);

    return {
        collage,
        definition,
        emergingfriction
    }
}
