import * as fs from 'mz/fs';
import * as winston from 'winston';

interface ApiConfig {
    username: string;
    hashedPassword: string;
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