


export default class APIError {
    /*{ statusCode: API_ERRORS.Network,
     thrower: "APIManager",
     name: error.name,
      message: error.message
       code : "default",
       context: error }*/
    constructor(public readonly statusCode: number,
        public readonly thrower: string,
        public readonly name: string,
        public readonly message: string,
        public readonly code: string,
        public readonly context: any){
    }

    static parse({ status_code, thrower, name, message, code, context}: any): APIError{
        return new APIError(status_code, thrower,name,message, code, context);
    }
}