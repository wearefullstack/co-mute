import Controller from "./Controller";
import CreateCarPoolOpportunityRoute, { FindByOwnerIDCarPoolOpportunityRoute, SearchCarPoolOpportunityRoute } from "./Routes/CarPoolOpportunity";
import Route from "./Routes/Route";


export type CarPoolOpportunityRoutes = "/create" | "/find_by_owner_id" | "/search";

export default
class CarPoolOpportunityController extends Controller<CarPoolOpportunityRoutes> {
    public path: string = "/car_pool_opportunity";
    public routes: Record<CarPoolOpportunityRoutes, Route<any>> = 
    {
        "/create": new CreateCarPoolOpportunityRoute(),
        "/find_by_owner_id": new FindByOwnerIDCarPoolOpportunityRoute(),
        "/search": new SearchCarPoolOpportunityRoute()
    }

}