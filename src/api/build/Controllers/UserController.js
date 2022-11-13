"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const Controller_1 = __importDefault(require("./Controller"));
const UserRoutes_1 = require("./Routes/UserRoutes");
class UserController extends Controller_1.default {
    constructor() {
        super(...arguments);
        this.path = "/users";
        this.routes = {
            "/register": new UserRoutes_1.RegisterUserRoute(),
            "/login": new UserRoutes_1.LoginUserRoute(),
            "/update": new UserRoutes_1.UpdateUserRoute()
        };
    }
}
exports.default = UserController;
//# sourceMappingURL=UserController.js.map