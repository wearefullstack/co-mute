import { Request } from "express";
import { ParamsDictionary } from "express-serve-static-core";
import { ValidationChain } from "express-validator";
import { ParsedQs } from "qs";
import Route from "./Route";
export declare class RegisterUserRoute extends Route<string> {
    path: string;
    validator: ValidationChain[];
    handle(request: Request<ParamsDictionary, any, any, ParsedQs, Record<string, any>>): Promise<string>;
}
export declare class LoginUserRoute extends Route<string> {
    path: string;
    validator: ValidationChain[];
    handle(request: Request<ParamsDictionary, any, any, ParsedQs, Record<string, any>>): Promise<string>;
}
export declare class UpdateUserRoute extends Route<string> {
    path: string;
    validator: ValidationChain[];
    handle(request: Request<ParamsDictionary, any, any, ParsedQs, Record<string, any>>): Promise<string>;
}
//# sourceMappingURL=UserRoutes.d.ts.map