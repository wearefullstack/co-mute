import { Request } from "express";
import { ParamsDictionary } from "express-serve-static-core";
import { body, ValidationChain } from "express-validator";
import { ParsedQs } from "qs";
import APIError from "../../APIError";
import JWTManager from "../../Managers/AuthenticationManager";
import User, { IUser } from "../../Models/User";
import Route from "./Route";


export
class RegisterUserRoute extends Route<IUser> {
    public path: string = "/register"
    public validator: ValidationChain[] =
    [   
        body("name").isString().isLength({ max: 32 }),
        body("surname").isString().isLength({ max: 32 }),
        body("phone").optional({checkFalsy: true}).isString().isLength({ max: 24 }),
        body("email").isString().isEmail().isLength({ max: 32}),
        body("password").isString()
    ]
    

    public handle(request: Request<ParamsDictionary, any, any, ParsedQs, Record<string, any>>): Promise<IUser> {
        const { name, surname, phone, email, password } = request.body;
        return User.register({ name, surname, phone, email }, password)
    }
}

export
class LoginUserRoute extends Route<string> {
    public path: string = "/login"
    public validator: ValidationChain[] =
    [   
        body("email").isString().isEmail().isLength({ max: 32}),
        body("password").isString()
    ]
    

    public async handle(request: Request<ParamsDictionary, any, any, ParsedQs, Record<string, any>>): Promise<string> {
        const { email, password } = request.body;
        const user = await User.loginUser(email, password);
        
        try {
            return JWTManager.getInstance().sign(user);
        } catch (error) {
            return Promise.reject(APIError.eServer("UserRoute").log(error));
        }
    }
}

export
class UpdateUserRoute extends Route<string> {
    public path: string = "/update"
    public validator: ValidationChain[] =
    [   
        body("name").isString().isLength({ max: 32 }),
        body("surname").isString().isLength({ max: 32 }),
        body("phone").optional({checkFalsy: true}).isString().isLength({ max: 24 }),
    ]
    

    public async handle(request: Request<ParamsDictionary, any, any, ParsedQs, Record<string, any>>): Promise<string> {
        const { name, surname, phone } = request.body;
        const authentication: IUser  = (request as any).authentication;
    
        const user = await (new User({...authentication, name, surname, phone })).updateUser();

        try {
            return JWTManager.getInstance().sign(user);
        } catch (error) {
            return Promise.reject(APIError.eServer("UserRoute").log(error));
        }
    }
}

