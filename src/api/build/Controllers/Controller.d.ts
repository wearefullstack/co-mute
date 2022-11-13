import { NextFunction, Request, RequestHandler, Response } from 'express';
import Route from './Routes/Route';
export interface IExpressController {
    path: string;
    handlers: RequestHandler[];
}
export default abstract class Controller<TRoutes extends string> {
    abstract path: string;
    abstract routes: Record<TRoutes, Route<any>>;
    control(routePath: TRoutes, authenticate?: boolean): IExpressController;
    authenticate(req: Request, res: Response, next: NextFunction): void;
}
//# sourceMappingURL=Controller.d.ts.map