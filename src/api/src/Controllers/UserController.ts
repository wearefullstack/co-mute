import { ValidationChain } from "express-validator";
import Controller from "./Controller";
import Route from "./Routes/Route";
import { LoginUserRoute, RegisterUserRoute, UpdateUserRoute } from "./Routes/UserRoutes";

type Routes = "/register" | "/login" | "/update";

export default
class UserController extends Controller<Routes> {
    public path: string = "/users";
    public routes: Record<Routes, Route<any>> = 
    {
        "/register" : new RegisterUserRoute(),
        "/login" : new LoginUserRoute(),
        "/update": new UpdateUserRoute()
    }
}