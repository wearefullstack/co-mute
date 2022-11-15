import dotenv from 'dotenv';
import MySQLManager from "./Managers/MySQLManager";
import express from 'express';
import cors from 'cors'
import UserController from './Controllers/UserController';
import CarPoolOpportunityController from './Controllers/CarPoolOpportunity';
import CarPoolConnectionController from './Controllers/CarPoolConnection';
import chalk from 'chalk';

const app = express();
dotenv.config();

app.use(express.json());
app.use(cors());


//Setup: controllers
const userController: UserController = new UserController();
const cpoController: CarPoolOpportunityController = new CarPoolOpportunityController();
const cpcContoller: CarPoolConnectionController = new CarPoolConnectionController();


//Setup: User endpoints:
const registerControl = userController.control("/register");
app.post(registerControl.path, ...registerControl.handlers);

const loginControl = userController.control("/login");
app.post(loginControl.path, ...loginControl.handlers);

const updateControl = userController.control("/update", true);
app.post(updateControl.path, ...updateControl.handlers);



//Setup: Car Pool Opportunities(CPOs) endpoints:
const cpoCreateControl = cpoController.control("/create", true);
app.post(cpoCreateControl.path, ...cpoCreateControl.handlers);

const cpoFindByOwnerIDControl = cpoController.control("/find_by_owner_id", true);
app.post(cpoFindByOwnerIDControl.path, ...cpoFindByOwnerIDControl.handlers);

const cpoSearchControl = cpoController.control("/search", true);
app.get(cpoSearchControl.path, ...cpoSearchControl.handlers);



//Setup: Car Pool Connections(CPCs) endpoints:
const cpcCreateControl = cpcContoller.control("/join", true);
app.post(cpcCreateControl.path, ...cpcCreateControl.handlers);

const cpcFindByUserIdController = cpcContoller.control("/find_by_user_id", true);
app.post(cpcFindByUserIdController.path, ...cpcFindByUserIdController.handlers);

const cpcLeaveController = cpcContoller.control("/leave", true);
app.post(cpcLeaveController.path, ...cpcLeaveController.handlers);



async function startServer(){
    await MySQLManager.init();
    const PORT = process.env.PORT || 8289;

   app.listen(PORT, () => {
    console.log(chalk.green(`✓ Server started on PORT: ${ PORT }`))
   })
}

startServer();



