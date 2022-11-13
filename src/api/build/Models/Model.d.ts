export declare type SaveMode = "Create" | "Update";
export declare type ModelWithID<TModel> = TModel & {
    id: string;
};
export default class Model<TModel extends {
    id: string;
}> {
    protected name: string;
    src: TModel;
    constructor(name: string, src: TModel);
    protected save(saveMode: SaveMode): Promise<TModel>;
    static findOne<TModel>(primaryKey: string, modelName: string, primaryKeyName?: string): Promise<ModelWithID<TModel> | null>;
    static find<TModel>(modelName: string, where?: string, args?: any[]): Promise<ModelWithID<TModel>[]>;
    private create;
    private update;
}
//# sourceMappingURL=Model.d.ts.map