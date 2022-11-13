"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class APIError {
    static eServer(thrower, message = "Internal Server Error") {
        return this.generate(500, thrower, message, null);
    }
    static eUnauthorized(thrower, message = "Unauthorized", errorCode = "0x0") {
        return this.generate(401, thrower, message, errorCode);
    }
    static eBadRequest(thrower, message = "Bad Request", errorCode = "0x0") {
        return this.generate(400, thrower, message, errorCode);
    }
    static eForbidden(thrower, message = "Forbidden", errorCode = "0x0") {
        return this.generate(403, thrower, message, errorCode);
    }
    static eConflict(thrower, message = "Conflict", errorCode = "0x0") {
        return this.generate(409, thrower, message, errorCode);
    }
    static eNotAcceptable(thrower, message = "Not Acceptable", errorCode = "0x0") {
        return this.generate(406, thrower, message, errorCode);
    }
    static eNotFound(thrower, message = "Not found", errorCode = "0x0") {
        const isString = typeof message == "string";
        return this.generate(404, thrower, isString ? message : `${message} Not Found`, errorCode);
    }
    static eGone(thrower, message = "Resource not available", errorCode = "0x0") {
        const isString = typeof message == "string";
        return this.generate(410, thrower, isString ? message : `${message} Not Found`, errorCode);
    }
    static eLegalReasons(thrower, message = "Unavailable For Legal Reasons", errorCode = "0x0") {
        const isString = typeof message == "string";
        return this.generate(451, thrower, isString ? message : `${message} Not Found`, errorCode);
    }
    static eParallelModification(thrower, message = "Parallel Modification", errorCode = "0x0") {
        const isString = typeof message == "string";
        return this.generate(441, thrower, isString ? message : `${message} Not Found`, errorCode);
    }
    static generate(statusCode, thrower, message, context) {
        const error = { status_code: statusCode, thrower, message, context };
        return {
            log: (systemError) => {
                console.log("logged error", systemError);
                return error;
            },
            toError: () => error
        };
    }
}
exports.default = APIError;
//# sourceMappingURL=APIError.js.map