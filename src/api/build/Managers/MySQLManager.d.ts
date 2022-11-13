import MySql from 'mysql';
export declare type IPromiseQuery<TResult> = (query: string, args: any) => Promise<TResult>;
export declare type ITransactionExecutor<TResult> = (connection: MySql.Connection, query: IPromiseQuery<any>) => Promise<TResult>;
export default class MySQLManager {
    private connection;
    private static INSTANCE;
    private static DEFAULT_MYSQL_PORT;
    constructor(connection: MySql.Connection);
    static init(): Promise<MySQLManager>;
    query<TResult>(query: string, args?: any): Promise<TResult>;
    withTransaction<TResult>(executor: ITransactionExecutor<TResult>): Promise<TResult>;
    static getInstance(): MySQLManager;
}
//# sourceMappingURL=MySQLManager.d.ts.map