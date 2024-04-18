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
        redirect(303, `/scenarios/${params.scenario}/steps/5-reflect`);
    },
    previous: async ({params, request}) => {
        save(params, request);
        redirect(303, `/scenarios/${params.scenario}/steps/3-whatif`);
    },
} satisfies Actions;

const save = async (params, request) => {
    console.log("Saving step 0")
    const scenario = params.scenario;
    const json = scenarios.json(scenario);

    const data = await request.formData();
    
    const collage = data.get('collage')
    json.friction.options.a.alternativeBackground = collage;

    const actor1name = data.get('actor1name')
    const actor1statement = data.get('actor1statement')
    // const actor1avatar = data.get('actor1avatar')
    // const actor1sprite = data.get('actor1sprite')
    json.actors[0].description = actor1name;
    json.actors[0].content.after.a = actor1statement
    // json.actors[0].avatar = actor1avatar
    // json.actors[0].sprite = actor1sprite;

    const actor2name = data.get('actor2name')
    const actor2statement = data.get('actor2statement')
    // const actor2avatar = data.get('actor2avatar')
    // const actor2sprite = data.get('actor2sprite')
    json.actors[1].description = actor2name;
    json.actors[1].content.after.a = actor2statement
    // json.actors[1].avatar = actor2avatar
    // json.actors[1].sprite = actor2sprite;

    const actor3name = data.get('actor3name')
    const actor3statement = data.get('actor3statement')
    // const actor3avatar = data.get('actor3avatar')
    // const actor3sprite = data.get('actor3sprite')
    json.actors[2].description = actor3name;
    json.actors[2].content.after.a = actor3statement
    // json.actors[2].avatar = actor3avatar
    // json.actors[2].sprite = actor3sprite;


    scenarios.save(scenario, json);
}
