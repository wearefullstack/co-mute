import dotenv from 'dotenv';
import MySQLManager from "./Managers/MySQLManager";
import User, { IUser } from './Models/User';
import express from 'express';
import UserController from './Controllers/UserController';
import Model from './Models/Model';
import CarPoolOpportunities from './Models/CarPoolOpportunity';

const app = express();

app.use(express.json());

dotenv.config();


const userController: UserController = new UserController();

const registerControl = userController.control("/register");
app.post(registerControl.path, ...registerControl.handlers);

const loginControl = userController.control("/login");
app.post(loginControl.path, ...loginControl.handlers);

const updateControl = userController.control("/update", true);
app.post(updateControl.path, ...updateControl.handlers);

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

    console.log(await CarPoolOpportunities.Create({
        days_available: "SAT ,SUN",
        departure_time: "10:30",
        destination: "Somewhere",
        expected_arrival_time: "15:00",
        origin: "s",
        available_seats: 2,
    }, "ccc04116-84fa-4d43-94d5-7d2e08efe97a"));



   // console.log(await CarPoolOpportunities.hasOverlayingJoinedCPOs("7:30", "08:30", "ccc04116-84fa-4d43-94d5-7d2e08efe97a"));

   app.listen(8289, () => {
    console.log("started..")
   })
}

startServer();



