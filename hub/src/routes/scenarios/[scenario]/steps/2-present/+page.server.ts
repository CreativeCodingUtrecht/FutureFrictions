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
        
    const collage = data.get('collage')
    json.scene.background = collage;

    saveImage(scenario, data, 'collageFile');

    const actor1name = data.get('actor1name')
    const actor1statement = data.get('actor1statement')
    // const actor1avatar = data.get('actor1avatar')
    const actor1sprite = data.get('actor1sprite')
    json.actors[0].description = actor1name;
    json.actors[0].content.before = actor1statement
    // json.actors[0].avatar = actor1avatar
    json.actors[0].sprite = actor1sprite;
    saveImage(scenario, data, 'actor1spriteFile');
    // saveImage(scenario, data, 'actor1avatarFile');

    const actor2name = data.get('actor2name')
    const actor2statement = data.get('actor2statement')
    // const actor2avatar = data.get('actor2avatar')
    const actor2sprite = data.get('actor2sprite')
    json.actors[1].description = actor2name;
    json.actors[1].content.before = actor2statement
    // json.actors[1].avatar = actor2avatar
    json.actors[1].sprite = actor2sprite;
    saveImage(scenario, data, 'actor2spriteFile');
    // saveImage(scenario, data, 'actor2avatarFile');

    const actor3name = data.get('actor3name')
    const actor3statement = data.get('actor3statement')
    // const actor3avatar = data.get('actor3avatar')
    const actor3sprite = data.get('actor3sprite')
    json.actors[2].description = actor3name;
    json.actors[2].content.before = actor3statement
    // json.actors[2].avatar = actor3avatar
    json.actors[2].sprite = actor3sprite;
    saveImage(scenario, data, 'actor3spriteFile');
    // saveImage(scenario, data, 'actor3avatarFile');

    const emergingfriction = data.get('emergingfriction')
    json.scene.content.emergingfriction = emergingfriction;
    
    scenarios.save(scenario, json);

    return {
        collage,
        actor1: {
            name: actor1name,
            statement: actor1statement,
            // avatar: actor1avatar,
            sprite: actor1sprite,
        },
        actor2: {
            name: actor2name,
            statement: actor2statement,
            // avatar: actor2avatar,
            sprite: actor2sprite,
        },
        actor3: {
            name: actor3name,
            statement: actor3statement,
            // avatar: actor3avatar,
            sprite: actor3sprite,
        },
        emergingfriction
    }
}
