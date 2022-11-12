import chalk from 'chalk';
import MySql from 'mysql';

export
type ITransactionExecutor<TResult> = ()=>Promise<TResult>;

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
                password: MYSQL_PASSWORD
            });

            
            process.on('SIGINT', ()=>{
                console.log(chalk.blue("Ⓘ Closing connection..."))
                connection.end();
            });

            console.log(chalk.blue("Ⓘ Connecting to MYSQL Server..."))
            connection.connect((error) => {
                if(error) reject(error)
                else {
                    console.log(chalk.green("✓ Connected."))
                    resolve(MySQLManager.INSTANCE = new MySQLManager(connection))
                }
            })
        });        
    }

    //help function for creating transactions
    withTransaction<TResult>(executor: ITransactionExecutor<TResult>){
        return new Promise<TResult>((resolve, reject) => {
            this.connection.beginTransaction((error) => {
                
                if(error){
                    reject(error);
                }else{
                    executor()
                    .then(result => this.connection.commit( error => {
                        if(error) throw error;
                        else resolve(result); 
                    }))
                    .catch((error)=>{
                        this.connection.rollback(_=> { throw error; });
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