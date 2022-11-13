
export interface Error{
    status_code: number,
    thrower: string
    message: string
    context: any
}

export default
class APIError{

    static eServer(thrower : string, message="Internal Server Error"){
        return this.generate(500, thrower, message, null);
    }

    static eUnauthorized(thrower : string, message="Unauthorized", errorCode="0x0"){
        return this.generate(401, thrower, message, errorCode);
    }

    static eBadRequest(thrower : string, message="Bad Request", errorCode="0x0"){
        return this.generate(400, thrower, message, errorCode);
    }

    static eForbidden(thrower : string, message="Forbidden", errorCode="0x0"){
        return this.generate(403, thrower, message, errorCode);
    }

    static eConflict(thrower : string, message="Conflict", errorCode="0x0"){
        return this.generate(409, thrower, message, errorCode);
    }

    static eNotAcceptable(thrower : string, message="Not Acceptable", errorCode="0x0"){
        return this.generate(406, thrower, message, errorCode);
    }

    static eNotFound(thrower : string, message : string | any ="Not found", errorCode="0x0"){
        const isString = typeof message == "string";

        return this.generate(404, thrower, isString ? message : `${ message } Not Found`, errorCode);
    }

    static eGone(thrower : string, message : string | any ="Resource not available", errorCode="0x0"){
        const isString = typeof message == "string";

        return this.generate(410, thrower, isString ? message : `${ message } Not Found`, errorCode);
    }

    static eLegalReasons(thrower : string, message : string | any ="Unavailable For Legal Reasons", errorCode="0x0"){
        const isString = typeof message == "string";

        return this.generate(451, thrower, isString ? message : `${ message } Not Found`, errorCode);
    }


    static eParallelModification(thrower : string, message : string = "Parallel Modification", errorCode="0x0"){
        const isString = typeof message == "string";

        return this.generate(441, thrower, isString ? message : `${ message } Not Found`, errorCode);
    }

    static generate<TContext>(statusCode: number, thrower : string, message : string, context : any){
        const error : Error = { status_code: statusCode, thrower, message, context };

        return {
            log: (systemError? : any)=> {
                console.log("logged error", systemError)
                return error;
            },
            toError: () => error
        }
    }
}