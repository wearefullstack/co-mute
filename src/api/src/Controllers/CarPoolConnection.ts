import Controller from "./Controller";
import { FindByUserIDRoute, JoinRoute, LeaveRoute } from "./Routes/CarPoolConnection";
import Route from "./Routes/Route";

type CPCRoutes = "/find_by_user_id" | "/join" | "/leave";

export default
class CarPoolConnectionController extends Controller<CPCRoutes> {
    public path: string = "/car_pool_connection";
    public routes: Record<CPCRoutes, Route<any>> =
    {
        "/join" : new JoinRoute(),
        "/leave": new LeaveRoute(),
        "/find_by_user_id": new FindByUserIDRoute()
    }

}