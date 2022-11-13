
import { NextFunction, Request, RequestHandler, Response } from 'express';
import { validationResult } from 'express-validator';
import APIError from '../APIError';
import JWTManager from '../Managers/AuthenticationManager';
import Route from './Routes/Route';

export interface IExpressController {
    path: string,
    handlers: RequestHandler[];
} 

export default
abstract class Controller<TRoutes extends string>{
    public abstract path: string;
    public abstract routes: Record<TRoutes, Route<any>>

    control(routePath: TRoutes,  authenticate: boolean = false): IExpressController {
        const route = this.routes[routePath];
        const reqHander: RequestHandler = (req, res) => {
            const errors = validationResult(req);
            if(errors.isEmpty())
            route.handle(req)
            .then(result => {
                res.status(200).json({ result });
            }).
            catch(error => {
                res.json({ error })
                console.log(error);
            })
            else
            res.json({error: errors.array()})
        };

        const handlers = authenticate ? [...route.validator, this.authenticate, reqHander] : [...route.validator, reqHander]
        return {
            path: this.path + route.path,
            handlers
        }
    }

    authenticate(req: Request, res: Response, next: NextFunction){
        const { authorization } = req.headers;
        if(authorization){
            const token = authorization.split(" ")[1];
            if(token){
                JWTManager.getInstance().verify(token)
                .then(payload => {
                    (req as any).authentication = payload;
                    next();
                })
                .catch(_ => {
                    const error = APIError.eServer("Contoller").toError();
                    res.status(error.status_code).json({ error });
                })
                return;
            }
        }

        const error = APIError.eUnauthorized("Controller").toError();
        res.status(error.status_code).json({ error });
    }
}