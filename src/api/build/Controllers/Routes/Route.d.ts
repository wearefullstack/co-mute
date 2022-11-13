import { ValidationChain } from "express-validator";
import { Request } from 'express';
export default abstract class Route<TResult> {
    abstract path: string;
    abstract validator: ValidationChain[];
    abstract handle(request: Request): Promise<TResult>;
}
//# sourceMappingURL=Route.d.ts.map