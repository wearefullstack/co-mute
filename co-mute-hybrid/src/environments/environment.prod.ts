import { AppConfig } from "src/app/abstractions/interfaces/app-config.interface";

export const environment : AppConfig = {
  production: true,
  api : {
    url : 'https://localhost:7244/api'
  }
};
