import type { Actions, PageServerLoad } from './$types';
import scenarios from '$lib/scenarios';
import { redirect } from '@sveltejs/kit';
import { backIn } from 'svelte/easing';

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
        redirect(303, `/scenarios/${params.scenario}/steps/5-reflect`);
    },
    previous: async ({params, request}) => {
        save(params, request);
        redirect(303, `/scenarios/${params.scenario}/steps/3-whatif`);
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
    console.log("Saving step 5")
    const scenario = params.scenario;
    const json = scenarios.json(scenario);

    const data = await request.formData();
    
    const collage = data.get('collage')
    json.friction.options.a.alternativeBackground = collage;
    saveImage(scenario, data, 'collageFile');

    // const actor1name = data.get('actor1name')
    const actor1statement = data.get('actor1statement')
    // const actor1avatar = data.get('actor1avatar')
    // const actor1sprite = data.get('actor1sprite')
    // json.actors[0].description = actor1name;
    json.actors[0].content.after.a = actor1statement;
    // json.actors[0].avatar = actor1avatar
    // json.actors[0].sprite = actor1sprite;

    // const actor2name = data.get('actor2name')
    const actor2statement = data.get('actor2statement')
    // const actor2avatar = data.get('actor2avatar')
    // const actor2sprite = data.get('actor2sprite')
    // json.actors[1].description = actor2name;
    json.actors[1].content.after.a = actor2statement
    // json.actors[1].avatar = actor2avatar
    // json.actors[1].sprite = actor2sprite;

    // const actor3name = data.get('actor3name')
    const actor3statement = data.get('actor3statement')
    // const actor3avatar = data.get('actor3avatar')
    // const actor3sprite = data.get('actor3sprite')
    // json.actors[2].description = actor3name;
    json.actors[2].content.after.a = actor3statement
    // json.actors[2].avatar = actor3avatar
    // json.actors[2].sprite = actor3sprite;

    const foregroundsprite1 = data.get('foregroundsprite1')
    json.friction.options.a.sprites.foreground[0] = foregroundsprite1;        
    saveImage(scenario, data, 'foregroundsprite1File');
    const foregroundsprite2 = data.get('foregroundsprite2')
    json.friction.options.a.sprites.foreground[1] = foregroundsprite2;
    saveImage(scenario, data, 'foregroundsprite2File');
    const foregroundsprite3 = data.get('foregroundsprite3')
    json.friction.options.a.sprites.foreground[2] = foregroundsprite3;
    saveImage(scenario, data, 'foregroundsprite3File');

    const backgroundsprite1 = data.get('backgroundsprite1')    
    json.friction.options.a.sprites.background[0] = backgroundsprite1;
    saveImage(scenario, data, 'backgroundsprite1File');
    const backgroundsprite2 = data.get('backgroundsprite2')
    json.friction.options.a.sprites.background[1] = backgroundsprite2;
    saveImage(scenario, data, 'backgroundsprite2File');
    const backgroundsprite3 = data.get('backgroundsprite3')
    json.friction.options.a.sprites.background[2] = backgroundsprite3;
    saveImage(scenario, data, 'backgroundsprite3File');

    const floatingsprite1 = data.get('floatingsprite1')    
    json.friction.options.a.sprites.floating[0] = floatingsprite1;
    saveImage(scenario, data, 'floatingsprite1File');
    const floatingsprite2 = data.get('floatingsprite2')
    json.friction.options.a.sprites.floating[1] = floatingsprite2;
    saveImage(scenario, data, 'floatingsprite2File');
    const floatingsprite3 = data.get('floatingsprite3')
    json.friction.options.a.sprites.floating[2] = floatingsprite3;
    saveImage(scenario, data, 'floatingsprite3File');

    scenarios.save(scenario, json);

    return {
        collage,
        actor1 : {
            statement: actor1statement,
        },
        actor2 : {
            statement: actor2statement,
        },
        actor3: {
            statement: actor3statement,
        },
        elements: {
            foreground: {
                sprite1: foregroundsprite1,
                sprite2: foregroundsprite2,
                sprite3: foregroundsprite3,
            },
            background: {
                sprite1 : backgroundsprite1,
                sprite2 : backgroundsprite2,
                sprite3 : backgroundsprite3,
            },
            floating: {
                sprite1 : floatingsprite1,
                sprite2 : floatingsprite2,
                sprite3 : floatingsprite3,
            }
        }
    }
}
