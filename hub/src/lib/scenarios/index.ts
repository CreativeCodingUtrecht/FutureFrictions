import fs from 'fs';
import { error } from '@sveltejs/kit';

const DATAROOT = "../data"

const list = () => {
    const scenarios = 
        fs.readdirSync(DATAROOT, { withFileTypes: true })
        .filter(dirent => dirent.isDirectory())
        .map(dirent => dirent.name)

    return scenarios
}

const json = (scenario : String) => {
    const filename = `${DATAROOT}/${scenario}/scenario.json`;

    if (!fs.existsSync(filename)) {
        console.log("File does not exist")
        throw error(404, {
            message: 'Requested scenario JSON does not exist',
        });
    }

    const data = fs.readFileSync(filename, 'utf8');

    return JSON.parse(data);
}

const save = (scenario: String, json: any) => {
    const filename = `${DATAROOT}/${scenario}/scenario.json`;

    if (!fs.existsSync(filename)) {
        console.log("File does not exist")
        throw error(404, {
            message: 'Requested scenario JSON does not exist',
        });
    }

    fs.writeFileSync(filename, JSON.stringify(json,null, 4));
}

export default {
    list,
    json,
    save
}
