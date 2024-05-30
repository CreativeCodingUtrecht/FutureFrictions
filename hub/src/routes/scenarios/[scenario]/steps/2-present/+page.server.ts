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

    if (!json.friction) {
        json.friction = {}
    }

    // Emerging friction
    const emergingfrictions = data.get('emergingfrictions')
    json.friction.emergingfrictions = emergingfrictions;
    
    // Collage PNG
    const collageFile = data.get('collageFile');
    
    console.log("CollageFile:", collageFile.name, collageFile.type);

    if (collageFile.name && collageFile.name.length > 0 && collageFile?.name !== "undefined") {
        console.log("Saving collage PNG as", collageFile.name)
        const filename = `${scenarios.SCENARIOROOT}/${scenario}/collage/present.png`;  
        console.log("filename", filename);
        const buffer = Buffer.from(await collageFile?.arrayBuffer());
        scenarios.addImage(scenario, filename, buffer);
        json.collage.present.url = `/api/scenarios/${scenario}/collage/present.png`;
    }

    scenarios.save(scenario, json);

    return {
        collage,
        definition,
        emergingfrictions
    }
}
