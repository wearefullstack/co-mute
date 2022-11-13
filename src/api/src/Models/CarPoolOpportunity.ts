import Model, { ModelWithID } from "./Model";
import moment from 'moment';
import APIError from "../APIError";
import { v4 as uuid } from 'uuid';
import MySQLManager from "../Managers/MySQLManager";
export interface Point {
    x: number,
    y: number
}

export interface ICarPoolOpportunity{
    id: string,
    departure_time: string,
    expected_arrival_time: string,
    origin: string,
    days_available: string,
    destination: string,
    available_seats: number,
    owner: string,
    notes?: string,
    date_created?: Date
}

export interface IJoinedCarPoolOpportunity{
    users_id: string,
    car_pool_opportunity_id: string,
    on_which_days: string,
    date_joined?: Date
}

export default
class CarPoolOpportunity extends Model<ICarPoolOpportunity> {
    public static readonly TABLE_NAME = "car_pool_opportunity";
  
    constructor(src: ICarPoolOpportunity){
        super(CarPoolOpportunity.TABLE_NAME, src)
    }

    public static async FindOne<TModel>(primaryKey: string, primaryKeyName?: string): Promise<CarPoolOpportunity | null> {
        const rawCPO = await Model.findOne<ICarPoolOpportunity>(primaryKey, this.TABLE_NAME, primaryKeyName);

        return rawCPO ? new CarPoolOpportunity(rawCPO) : null;
    }

    static async Create(src: Omit<ICarPoolOpportunity, "id" | "owner" | "date_created">, owner: string){
        return MySQLManager.getInstance()
        .withTransaction<ICarPoolOpportunity>(async (connection, Query) => {
            const mDepTime = moment(src.departure_time, "HH:mm:ss", false),
            mEATTime = moment(src.expected_arrival_time, "HH:mm:ss", false);
    
            if(mDepTime.isBefore(mEATTime)){
                const days: string[] = src.days_available.split(",");
                if(!(await this.hasOverlappingCPCsOrCPOs(src.departure_time, src.expected_arrival_time, days, owner))){
                    const id: string = uuid();
                    const CPO: ICarPoolOpportunity = {id, owner, ...src };
                    (new CarPoolOpportunity(CPO)).save("Create");
                    return CPO;
                }else{
                    return Promise.reject(APIError.eForbidden("This Car Pools time range overlapps with one of your created/join Car Pools", "0x1").toError())
                }            
            }else{
                return Promise.reject(APIError.eForbidden("Departure Time cannot be after Expected Arrival Time.", '0x0').toError());
            }
        })
    }

    static async hasOverlappingCPCsOrCPOs(departure_time: string, expected_arrival_time: string, days: string[], owner: string){
        return await this.hasOverlayingCreatedCPOs(departure_time, expected_arrival_time, days, owner)  || await this.hasOverlayingJoinedCPOs(departure_time, expected_arrival_time, days, owner);
    }


    static async hasOverlayingCreatedCPOs(departure_time: string, expected_arrival_time: string, days: string[], owner: string){
        const args = [ departure_time, departure_time, expected_arrival_time, expected_arrival_time, departure_time, expected_arrival_time, departure_time, expected_arrival_time, owner];

        const whereCondition = `(${ CarPoolOpportunity.createIsOverlayingCondition() }) AND owner=?;`;
        
        const overlappingCPOs = await Model.find<ICarPoolOpportunity>(CarPoolOpportunity.TABLE_NAME, whereCondition, args);
        if(overlappingCPOs.length > 0){
            return overlappingCPOs.some(overlappingCPO => {
                return this.hasOverlappingDays(days, overlappingCPO.days_available.split(','));
            })
        }else{
            return false;
        }
    }


    static hasOverlappingDays(days0: string[], days1: string[]){
        return days0.some(day0 => {
            const stdDay0 = day0.trim().toLowerCase();
            return days1.some(day1 => {
                const stdDay1 = day1.trim().toLowerCase();
                return stdDay0 == stdDay1;
            })
        })
    }


    static async hasOverlayingJoinedCPOs(departure_time: string, expected_arrival_time: string, days: string[], user: string){
        const args = [ user, departure_time, departure_time, expected_arrival_time, expected_arrival_time, departure_time, expected_arrival_time, departure_time, expected_arrival_time];

        const tables = 'car_pool_opportunity, users_has_car_pool_opportunity as JCPO';

        const whereCondition = `JCPO.car_pool_opportunity_id=id AND JCPO.users_id=? AND (${ this.createIsOverlayingCondition() })`;
        const overlappingCPOs = await Model.find<IJoinedCarPoolOpportunity>(tables, whereCondition, args);

        if(overlappingCPOs.length > 0){
            return overlappingCPOs.some(overlappingCPO => {
                return this.hasOverlappingDays(days, overlappingCPO.on_which_days.split(','));
            })
        }else{
            return false;
        }
    }


    static createIsOverlayingCondition(){
        const DTWithin = `departure_time <  ? AND expected_arrival_time > ?`;
        const EATWithin = `departure_time < ? AND expected_arrival_time > ?`;
        const _DTWithin = `? < departure_time AND ? > departure_time`;
        const _EATWithin = `? < expected_arrival_time AND ? > expected_arrival_time`;
        
        return `${ DTWithin } OR ${ EATWithin } OR ${ _DTWithin } OR ${ _EATWithin }`;
    }

    static findByOwnerID(ownerID: string){
        return Model.find(CarPoolOpportunity.TABLE_NAME, "owner=?", [ ownerID ]);
    }

    static search(searchTerm: string){
        return MySQLManager.getInstance()
        .withTransaction<ICarPoolOpportunity[]>(async (connection, queryExecutor) => {
            
            const query: string = `SELECT * FROM ${ CarPoolOpportunity.TABLE_NAME } WHERE (origin LIKE ?) OR (destination LIKE ?);`;
            return queryExecutor(query, [ searchTerm ]); 
        })
    }


}