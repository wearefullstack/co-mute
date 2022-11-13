import { Request } from "express";
import { ParamsDictionary } from "express-serve-static-core";
import { body, ValidationChain } from "express-validator";
import moment from "moment";
import { ParsedQs } from "qs";
import CarPoolOpportunity, { ICarPoolOpportunity } from "../../Models/CarPoolOpportunity";
import { IUser } from "../../Models/User";
import Route from "./Route";


export default
class CreateCarPoolOpportunityRoute extends Route<ICarPoolOpportunity>{
    public path: string = "/create";
    public validator: ValidationChain[] =
    [
        body("departure_time").isString().trim().custom(this.validateTime),
        body("expected_arrival_time").isString().trim().custom(this.validateTime),
        body("origin").isString().isLength({ max: 45 }),
        body("days_available").isString().isLength({ max: 28 }),
        body("destination").isString().isLength({ max: 45 }),
        body("available_seats").isInt({ min: 0 }),
        body("notes").optional({ checkFalsy: true }).isString()
    ]

    public handle(request: Request<ParamsDictionary, any, any, ParsedQs, Record<string, any>>): Promise<ICarPoolOpportunity> {
        const { id }: IUser  = (request as any).authentication;
        const {departure_time, expected_arrival_time, origin, days_available, destination, available_seats, notes}: ICarPoolOpportunity = request.body;

        return CarPoolOpportunity.Create({ departure_time, expected_arrival_time, origin, days_available, destination, available_seats, notes }, id);
    }

    validateTime(value: any){
        const mTime = moment(`${ value }`, "HH:mm:ss", true);
        
    
        return mTime.isValid();
    }

}


export 
class FindByOwnerIDCarPoolOpportunityRoute extends Route<ICarPoolOpportunity[]> {
    public path: string = "/find_by_owner_id";
    public validator: ValidationChain[] = [];

    public handle(request: Request<ParamsDictionary, any, any, ParsedQs, Record<string, any>>): Promise<ICarPoolOpportunity[]> {
        const { id }: IUser  = (request as any).authentication;
        return CarPoolOpportunity.findByOwnerID(id);
    }

}

export 
class SearchCarPoolOpportunityRoute extends Route<ICarPoolOpportunity[]> {
    public path: string = "/search";
    public validator: ValidationChain[] = [];

    public handle(request: Request<ParamsDictionary, any, any, ParsedQs, Record<string, any>>): Promise<ICarPoolOpportunity[]> {
        const { id }: IUser  = (request as any).authentication;
        const { location } = request.query;
        return CarPoolOpportunity.search(`${ location }` || "",id)
    }

}