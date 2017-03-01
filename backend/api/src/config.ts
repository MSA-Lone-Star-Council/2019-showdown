import * as fs from 'fs';
import * as winston from 'winston';

interface FacebookConfig {
    appID: string,
    appSecret: string,
}

interface ApiConfig {
    secret: string,
    facebook: FacebookConfig,
}

interface AppConfig {
    api: ApiConfig;
}

export function loadConfig(filename: string): AppConfig {
    const configBuffer = fs.readFileSync(filename);
    const configString = String.fromCharCode.apply(null, new Uint16Array(configBuffer));
    const config : AppConfig = JSON.parse(configString);
    return config;
}

let _config = null;
export function config(filename: string = '/usr/config/config.json'): AppConfig {
    if(_config === null){
        winston.info(`Loading config file from ${ filename }`)
        _config = loadConfig(filename);
    }
    return _config;
}

const configPath: string = process.env.CONFIG_FILE || '/usr/config/config.json';
const appConfig = config(configPath);
export default appConfig;