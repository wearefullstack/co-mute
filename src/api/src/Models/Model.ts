import MySQLManager, { IPromiseQuery } from "../Managers/MySQLManager"

export type SaveMode = "Create" | "Update"
export type ModelWithID<TModel> = TModel & { id: string };

export default
class Model<TModel extends { id: string }> {
    constructor(protected name: string, public src: TModel){};

    protected save(saveMode: SaveMode){
        return MySQLManager.getInstance()
        .withTransaction<TModel>(async (connection, Query) => {
            await (saveMode === "Create"? this.create(Query): this.update(Query));
            return this.src;
        })
    }

    
    public static findOne<TModel>(primaryKey: string, modelName: string, primaryKeyName = "id"){
        return MySQLManager.getInstance()
        .withTransaction<ModelWithID<TModel> | null >(async (connection, queryExecutor) => {
            const query: string = `SELECT * FROM ${ modelName } WHERE ${ primaryKeyName }=?;`;
            const models: any[] = await queryExecutor(query, [ primaryKey ]);
            
            return models.length ===  0 ? null : models[0];
        })
    }

    public static find<TModel>(modelName: string, where?: string , args?: any[]){
        return MySQLManager.getInstance()
        .withTransaction<ModelWithID<TModel>[]>(async (connection, queryExecutor) => {
            const query: string = `SELECT * FROM ${ modelName } ${ where ? `WHERE ${ where }`: ``};`;
            const models: any[] = await queryExecutor(query, args);
            
            return models;
        })
    }

    private create(Query: IPromiseQuery<TModel>){
        const args: any[] = [];
        const keys = Object.keys(this.src);

        const filteredKeys = keys.filter(key => {
            
            if(this.src[key] !== undefined){
                args.push(this.src[key])
                return true;
            }else{
                return false;
            }
        })

        const cols = `(${ filteredKeys.join(",") })`;
        const query: string = `INSERT INTO ${ this.name } ${ cols } VALUES (?)`;
        return Query(query, [args]);
    }

    private update(Query: IPromiseQuery<TModel>){
        const clone: Partial<TModel> = {...this.src};
        delete clone.id;

        const query = `UPDATE ${ this.name } SET ? WHERE id=?`;
        return Query(query, [clone, this.src.id])
    }
}