import UserManager, { IUser } from "./UserManager";
import APIError from "../APIError";

export const API_ERRORS = {
    Network: -1,
    Unknown: -2
}

export default
class APIManager {
    private static INSTANCE: APIManager;
    
    getCreatedCPOs(){
        return this.execute("/car_pool_opportunity/find_by_owner_id", "POST", {}, true);
    }

    createCPO(cpo: any){
        return this.execute("/car_pool_opportunity/create", "POST", {...cpo, days_available: cpo.days_available.join(",")});
    }

    registerUser(user: Omit<IUser, "id" | "date_created">): any{
        return this.execute("/users/register",  "POST", user);
    }


    async execute(path : string, method : string, data : any = {}, includeToken = true){
         console.log(path, method, data, includeToken);
        const headers : any =  {};
        if(includeToken){
            headers["authorization"] = `bearer ${ UserManager.getInstance().getActiveToken() }`;
        }
        headers['Content-Type'] = 'application/json';

        const config : any = { 
            method,
            headers 
        }
        

        var url = process.env.REACT_APP_API_HOST + path;

        if(method != "GET"){
            config["body"] = JSON.stringify(data);
        }else{
            url = url + "?" + new URLSearchParams(data)
        }
        
        
        return fetch(url, config)
        .then(response =>  Promise.all([ response, response.json()]))
        .catch(error => {
            console.log("-e-s", JSON.stringify(error, undefined, "  "),error);
            var _error: any;
            if(error instanceof TypeError){
                _error = new APIError(API_ERRORS.Network, "APIManager", error.name, error.message, "default", error);
            }else{
                _error = new APIError(API_ERRORS.Unknown, "APIManager", "FetchException", "Error occured while fetching", "default", error);
            }
            return Promise.reject(_error);                
        })
        .then(([response, result]) => {
            if(response.ok){
                return result;
            }else{
                 return Promise.reject(APIError.parse(result["error"]));
            }
        })
    }

    static getInstance(){
        return APIManager.INSTANCE || ( APIManager.INSTANCE = new APIManager());
    }

}