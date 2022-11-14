import chalk from 'chalk';
import { connect } from 'http2';
import MySql from 'mysql';


export 
type IPromiseQuery<TResult> = (query: string, args: any) => Promise<TResult>;

export
type ITransactionExecutor<TResult> = (connection: MySql.Connection, query: IPromiseQuery<any>)=>Promise<TResult>;

//singleton class to manage mysql connection
export default
class  MySQLManager {
    private static INSTANCE: MySQLManager;
    private static DEFAULT_MYSQL_PORT: string = "3306";

    constructor(private connection: MySql.Connection){
        
    }

    //creates connection to the MySql Instance. should be run once before calling get instance.
    static async init(): Promise<MySQLManager>{
        
        return new Promise<MySQLManager>((resolve, reject) => {
            const { MYSQL_HOST, MYSQL_PORT, MYSQL_DATABASE, MYSQL_USER, MYSQL_PASSWORD } = process.env;
            console.log(chalk.blue("Ⓘ Creating connection..."))
            const connection = MySql.createConnection({
                host: MYSQL_HOST,
                port: parseInt(MYSQL_PORT || this.DEFAULT_MYSQL_PORT),
                database: MYSQL_DATABASE,
                user: MYSQL_USER,
                password: MYSQL_PASSWORD,
            });

            
            process.on('SIGINT', ()=>{
                console.log(chalk.red("Ⓘ Closing connection..."))
                connection.end();
            });

            console.log(chalk.blue("Ⓘ Connecting to MYSQL Server..."))
     
            connection.connect((error) => {
                
                if(error) reject(error)
                else {
                    connection.on("error", (error)=>{
                        console.log("mysql-error", error);
                    })
                    console.log(chalk.green("✓ Connected."))
                    resolve(MySQLManager.INSTANCE = new MySQLManager(connection))
                }
            })
        });        
    }

    //helper function for converting the connection.query function into a promise function.
    query<TResult>(query: string, args?: any){
        return new Promise<TResult>((resolve, reject) => {
            const q = this.connection.query(query, args, (error, results) => {
                if(error) reject(error);
                else resolve(results);
            });
            console.log(chalk.blue("[i] SQL Query:" + q.sql))
        })
    }

    //helper function for creating transactions
    withTransaction<TResult>(executor: ITransactionExecutor<TResult>){
       
        return new Promise<TResult>((resolve, reject) => {
            this.connection.beginTransaction((error) => {
                
                if(error){
                    reject(error);
                }else{
                    executor(this.connection, this.query.bind(this))
                    .then(result => this.connection.commit( error => {
                        if(error) throw error;
                        else resolve(result); 
                    }))
                    .catch((error)=>{
                        this.connection.rollback(_=> { });
                        reject(error);
                    })
                }


            })
        });
    }

    static getInstance(): MySQLManager {
        if(MySQLManager.INSTANCE)
            return MySQLManager.INSTANCE;

        throw new Error("MySQLManager is not initialized.");
    }


}