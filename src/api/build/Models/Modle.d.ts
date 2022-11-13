export declare type SaveMode = "Create" | "Update";
export default class Model<TModel extends {
    id: string;
}> {
    private name;
    src: TModel;
    constructor(name: string, src: TModel);
    save(saveMode: SaveMode): void;
}
//# sourceMappingURL=Modle.d.ts.map