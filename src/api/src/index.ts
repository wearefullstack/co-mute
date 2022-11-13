import dotenv from 'dotenv';
import MySQLManager from "./Managers/MySQLManager";
import User, { IUser } from './Models/User';
import express from 'express';
import UserController from './Controllers/UserController';
import Model from './Models/Model';
import CarPoolOpportunity from './Models/CarPoolOpportunity';
import CarPoolOpportunityController from './Controllers/CarPoolOpportunity';
import CarPoolConnectionController from './Controllers/CarPoolConnection';

const app = express();

app.use(express.json());

dotenv.config();


const userController: UserController = new UserController();
const cpoController: CarPoolOpportunityController = new CarPoolOpportunityController();
const cpcContoller: CarPoolConnectionController = new CarPoolConnectionController();


// user
const registerControl = userController.control("/register");
app.post(registerControl.path, ...registerControl.handlers);

const loginControl = userController.control("/login");
app.post(loginControl.path, ...loginControl.handlers);

const updateControl = userController.control("/update", true);
app.post(updateControl.path, ...updateControl.handlers);


//cpo
const cpoCreateControl = cpoController.control("/create", true);
app.post(cpoCreateControl.path, ...cpoCreateControl.handlers);

const cpoFindByOwnerIDControl = cpoController.control("/find_by_owner_id", true);
console.log("::p", cpoFindByOwnerIDControl.path)
app.post(cpoFindByOwnerIDControl.path, ...cpoFindByOwnerIDControl.handlers);

const cpoSearchControl = cpoController.control("/search", true);
app.get(cpoSearchControl.path, ...cpoSearchControl.handlers);

//cpc
const cpcCreateControl = cpcContoller.control("/join", true);
app.post(cpcCreateControl.path, ...cpcCreateControl.handlers);

const cpcFindByUserIdController = cpcContoller.control("/find_by_user_id", true);
app.post(cpcFindByUserIdController.path, ...cpcFindByUserIdController.handlers);

const cpcLeaveController = cpcContoller.control("/leave", true);
app.post(cpcLeaveController.path, ...cpcLeaveController.handlers);







async function startServer(){
    await MySQLManager.init();

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

    /*console.log(await CarPoolOpportunity.Create({
        days_available: "SAT ,SUN",
        departure_time: "10:30",
        destination: "Somewhere",
        expected_arrival_time: "15:00",
        origin: "s",
        available_seats: 2,
    }, "ccc04116-84fa-4d43-94d5-7d2e08efe97a"));*/



   // console.log(await CarPoolOpportunities.hasOverlayingJoinedCPOs("7:30", "08:30", "ccc04116-84fa-4d43-94d5-7d2e08efe97a"));

   app.listen(8289, () => {
    console.log("started..")
   })
}

startServer();



