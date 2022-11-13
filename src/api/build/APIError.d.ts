export interface Error {
    status_code: number;
    thrower: string;
    message: string;
    context: any;
}
export default class APIError {
    static eServer(thrower: string, message?: string): {
        log: (systemError?: any) => Error;
        toError: () => Error;
    };
    static eUnauthorized(thrower: string, message?: string, errorCode?: string): {
        log: (systemError?: any) => Error;
        toError: () => Error;
    };
    static eBadRequest(thrower: string, message?: string, errorCode?: string): {
        log: (systemError?: any) => Error;
        toError: () => Error;
    };
    static eForbidden(thrower: string, message?: string, errorCode?: string): {
        log: (systemError?: any) => Error;
        toError: () => Error;
    };
    static eConflict(thrower: string, message?: string, errorCode?: string): {
        log: (systemError?: any) => Error;
        toError: () => Error;
    };
    static eNotAcceptable(thrower: string, message?: string, errorCode?: string): {
        log: (systemError?: any) => Error;
        toError: () => Error;
    };
    static eNotFound(thrower: string, message?: string | any, errorCode?: string): {
        log: (systemError?: any) => Error;
        toError: () => Error;
    };
    static eGone(thrower: string, message?: string | any, errorCode?: string): {
        log: (systemError?: any) => Error;
        toError: () => Error;
    };
    static eLegalReasons(thrower: string, message?: string | any, errorCode?: string): {
        log: (systemError?: any) => Error;
        toError: () => Error;
    };
    static eParallelModification(thrower: string, message?: string, errorCode?: string): {
        log: (systemError?: any) => Error;
        toError: () => Error;
    };
    static generate<TContext>(statusCode: number, thrower: string, message: string, context: any): {
        log: (systemError?: any) => Error;
        toError: () => Error;
    };
}
//# sourceMappingURL=APIError.d.ts.map