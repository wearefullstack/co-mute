import MySql from 'mysql';
export declare type ITransactionExecutor<TResult> = () => Promise<TResult>;
export default class MySQLManager {
    private connection;
    private static INSTANCE;
    private static DEFAULT_MYSQL_PORT;
    constructor(connection: MySql.Connection);
    static init(): Promise<MySQLManager>;
    withTransaction<TResult>(executor: ITransactionExecutor<TResult>): Promise<TResult>;
    static getInstance(): MySQLManager;
}
//# sourceMappingURL=MySQLManager.d.ts.map