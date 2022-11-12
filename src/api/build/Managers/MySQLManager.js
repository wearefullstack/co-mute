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
const chalk_1 = __importDefault(require("chalk"));
const mysql_1 = __importDefault(require("mysql"));
//singleton class to manage mysql connection
class MySQLManager {
    constructor(connection) {
        this.connection = connection;
    }
    //creates connection to the MySql Instance. should be run once before calling get instance.
    static init() {
        return __awaiter(this, void 0, void 0, function* () {
            return new Promise((resolve, reject) => {
                const { MYSQL_HOST, MYSQL_PORT, MYSQL_DATABASE, MYSQL_USER, MYSQL_PASSWORD } = process.env;
                console.log(chalk_1.default.blue("Ⓘ Creating connection..."));
                const connection = mysql_1.default.createConnection({
                    host: MYSQL_HOST,
                    port: parseInt(MYSQL_PORT || this.DEFAULT_MYSQL_PORT),
                    database: MYSQL_DATABASE,
                    user: MYSQL_USER,
                    password: MYSQL_PASSWORD
                });
                process.on('SIGINT', () => {
                    console.log(chalk_1.default.blue("Ⓘ Closing connection..."));
                    connection.end();
                });
                console.log(chalk_1.default.blue("Ⓘ Connecting to MYSQL Server..."));
                connection.connect((error) => {
                    if (error)
                        reject(error);
                    else {
                        console.log(chalk_1.default.green("✓ Connected."));
                        resolve(MySQLManager.INSTANCE = new MySQLManager(connection));
                    }
                });
            });
        });
    }
    //help function for creating transactions
    withTransaction(executor) {
        return new Promise((resolve, reject) => {
            this.connection.beginTransaction((error) => {
                if (error) {
                    reject(error);
                }
                else {
                    executor()
                        .then(result => this.connection.commit(error => {
                        if (error)
                            throw error;
                        else
                            resolve(result);
                    }))
                        .catch((error) => {
                        this.connection.rollback(_ => { throw error; });
                        reject(error);
                    });
                }
            });
        });
    }
    static getInstance() {
        if (MySQLManager.INSTANCE)
            return MySQLManager.INSTANCE;
        throw new Error("MySQLManager is not initialized.");
    }
}
exports.default = MySQLManager;
MySQLManager.DEFAULT_MYSQL_PORT = "3306";
//# sourceMappingURL=MySQLManager.js.map