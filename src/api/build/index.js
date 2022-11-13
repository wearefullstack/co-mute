"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const dotenv_1 = __importDefault(require("dotenv"));
const MySQLManager_1 = __importDefault(require("./Managers/MySQLManager"));
const express_1 = __importDefault(require("express"));
const UserController_1 = __importDefault(require("./Controllers/UserController"));
const CarPoolOpportunity_1 = __importDefault(require("./Models/CarPoolOpportunity"));
const app = express_1.default();
app.use(express_1.default.json());
dotenv_1.default.config();
const userController = new UserController_1.default();
const registerControl = userController.control("/register");
app.post(registerControl.path, ...registerControl.handlers);
const loginControl = userController.control("/login");
app.post(loginControl.path, ...loginControl.handlers);
const updateControl = userController.control("/update", true);
app.post(updateControl.path, ...updateControl.handlers);
function startServer() {
    return __awaiter(this, void 0, void 0, function* () {
        yield MySQLManager_1.default.init();
        /*const user: User = new User({
            id: "a",
            name: "as",
            surname: "n",
            phone: "0603627175",
            email: "b@e.com",
            password_hash: "#",
            password_salt: 12
        });
        
        user.save("Update");*/
        /* User.register({
             name: "as",
             surname: "n",
             phone: "0603627175",
             email: "b@e.codm",
         }, "test");*/
        console.log(yield CarPoolOpportunity_1.default.Create({
            days_available: "SAT ,SUN",
            departure_time: "10:30",
            destination: "Somewhere",
            expected_arrival_time: "15:00",
            origin: "s",
            available_seats: 2,
        }, "ccc04116-84fa-4d43-94d5-7d2e08efe97a"));
        // console.log(await CarPoolOpportunities.hasOverlayingJoinedCPOs("7:30", "08:30", "ccc04116-84fa-4d43-94d5-7d2e08efe97a"));
        app.listen(8289, () => {
            console.log("started..");
        });
    });
}
startServer();
//# sourceMappingURL=index.js.map