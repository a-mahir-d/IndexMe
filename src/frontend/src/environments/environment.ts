import { dev } from './dev';
import { prod } from './prod';

// const env = prod;
const env = dev;

export const environment = {
  serverUrl: env.serverUrl
};