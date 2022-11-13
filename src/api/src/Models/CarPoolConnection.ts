import APIError from "../APIError";
import MySQLManager from "../Managers/MySQLManager";
import CarPoolOpportunity, { ICarPoolOpportunity, IJoinedCarPoolOpportunity } from "./CarPoolOpportunity";
import Model from "./Model";



export default
class CarPoolConnection {
    private static TABLE_NAME: string = process.env.CPC_TABLE_NAME || "_";
    

    static async join(cpoID: string, userID: string, onWhichDays: string){
        return MySQLManager.getInstance()
        .withTransaction<IJoinedCarPoolOpportunity>(async(connection, queryExecutor) => {

            await this.checkEligibility(cpoID, userID, onWhichDays);
            const query = `INSERT INTO ${ CarPoolConnection.TABLE_NAME }(users_id, car_pool_opportunity_id, on_which_day) VALUES(?,?,?)`;
            const args: any[] = [userID, cpoID, onWhichDays]
            return queryExecutor(query, args);
        })        
    }

    private static async checkEligibility(cpoID: string, userID: string, onWhichDays: string): Promise<true> {
        const cpo: CarPoolOpportunity | null = await CarPoolOpportunity.FindOne(cpoID);

        if(cpo){
            const hasAvailableSeats: boolean = await CarPoolConnection.hasAvailableSeats(cpoID, cpo.src.available_seats);
            if(hasAvailableSeats){
                const { departure_time, expected_arrival_time } = cpo.src;
                const hasOverlappingCPCsOrCPOs: boolean = await CarPoolOpportunity.hasOverlappingCPCsOrCPOs(departure_time, expected_arrival_time, onWhichDays.split(","), userID);
                if(!hasOverlappingCPCsOrCPOs){
                    return true;
                }else
                    return Promise.reject(APIError.eForbidden("CarPoolConnection", "The requested Car Pool time range overlaps with a Car Pool you Joined or Created"))
            }else
                return Promise.reject(APIError.eForbidden("CarPoolConnection", "The requested Car Pool doesn't have any seats available."));
        }else
            return Promise.reject(APIError.eNotFound("CarPoolConnection", "The requested Car Pool was not found."));
        
    }

    static async hasAvailableSeats(cpoID:string, maxSeats: number){
        const cpos: IJoinedCarPoolOpportunity[] = await CarPoolConnection.find(cpoID);
        return (maxSeats - cpos.length) !== 0;
    }

    static find(cpoID: string){
        const whereCondition: string = `car_pool_opportunity_id=?`;
        return Model.find<IJoinedCarPoolOpportunity>(CarPoolConnection.TABLE_NAME, whereCondition, [ cpoID ]);
    }
}