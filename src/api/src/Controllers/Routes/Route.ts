import { ValidationChain } from "express-validator";
import { Request } from 'express';

export default
abstract class Route<TResult> {
    public abstract path: string;
    public abstract validator: ValidationChain[];
    public abstract handle(request: Request): Promise<TResult>;
}