import Controller from "./Controller";
import Route from "./Routes/Route";
declare type Routes = "/register" | "/login" | "/update";
export default class UserController extends Controller<Routes> {
    path: string;
    routes: Record<Routes, Route<any>>;
}
export {};
//# sourceMappingURL=UserController.d.ts.map