import { Request } from "express";
import { ParamsDictionary } from "express-serve-static-core";
import { body, ValidationChain } from "express-validator";
import { ParsedQs } from "qs";
import CarPoolConnection from "../../Models/CarPoolConnection";
import { IJoinedCarPoolOpportunity } from "../../Models/CarPoolOpportunity";
import { IUser } from "../../Models/User";
import Route from "./Route";


export
class JoinRoute extends Route<IJoinedCarPoolOpportunity> {
    public path: string = "/join";

    public validator: ValidationChain[] =
    [
        body("cpo_id").isString(),
        body("on_which_days").isString().isLength({ max: 28 })
    ]

    public handle(request: Request<ParamsDictionary, any, any, ParsedQs, Record<string, any>>): Promise<IJoinedCarPoolOpportunity> {
        const { id }: IUser  = (request as any).authentication;
        const { cpo_id, on_which_days } = request.body;

        return CarPoolConnection.join(`${cpo_id}`, id, `${on_which_days}`)
    }
}

export
class LeaveRoute extends Route<true> {
    public path: string = "/leave";

    public validator: ValidationChain[] =
    [
        body("cpo_id").isString(),
    ]

    public handle(request: Request<ParamsDictionary, any, any, ParsedQs, Record<string, any>>): Promise<true> {
        const { id }: IUser  = (request as any).authentication;
        const { cpo_id } = request.body;

        return CarPoolConnection.leave(`${cpo_id}`, id)
    }

}

export
class FindByUserIDRoute extends Route<IJoinedCarPoolOpportunity[]> {
    public path: string = "/find_by_user_id";

    public validator: ValidationChain[] = [];

    public handle(request: Request<ParamsDictionary, any, any, ParsedQs, Record<string, any>>): Promise<IJoinedCarPoolOpportunity[]> {
        const { id }: IUser  = (request as any).authentication;

        return CarPoolConnection.findByUserID(id);
    }

}