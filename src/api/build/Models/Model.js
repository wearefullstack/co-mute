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
const MySQLManager_1 = __importDefault(require("../Managers/MySQLManager"));
class Model {
    constructor(name, src) {
        this.name = name;
        this.src = src;
    }
    ;
    save(saveMode) {
        return MySQLManager_1.default.getInstance()
            .withTransaction((connection, Query) => __awaiter(this, void 0, void 0, function* () {
            yield (saveMode === "Create" ? this.create(Query) : this.update(Query));
            return this.src;
        }));
    }
    static findOne(primaryKey, modelName, primaryKeyName = "id") {
        return MySQLManager_1.default.getInstance()
            .withTransaction((connection, queryExecutor) => __awaiter(this, void 0, void 0, function* () {
            const query = `SELECT * FROM ${modelName} WHERE ${primaryKeyName}=?;`;
            const models = yield queryExecutor(query, [primaryKey]);
            return models.length === 0 ? null : models[0];
        }));
    }
    static find(modelName, where, args) {
        return MySQLManager_1.default.getInstance()
            .withTransaction((connection, queryExecutor) => __awaiter(this, void 0, void 0, function* () {
            const query = `SELECT * FROM ${modelName} ${where ? `WHERE ${where}` : ``};`;
            const models = yield queryExecutor(query, args);
            return models;
        }));
    }
    create(Query) {
        const args = [];
        const keys = Object.keys(this.src);
        const filteredKeys = keys.filter(key => {
            if (this.src[key] !== undefined) {
                args.push(this.src[key]);
                return true;
            }
            else {
                return false;
            }
        });
        const cols = `(${filteredKeys.join(",")})`;
        const query = `INSERT INTO ${this.name} ${cols} VALUES (?)`;
        return Query(query, [args]);
    }
    update(Query) {
        const clone = Object.assign({}, this.src);
        delete clone.id;
        const query = `UPDATE ${this.name} SET ? WHERE id=?`;
        return Query(query, [clone, this.src.id]);
    }
}
exports.default = Model;
//# sourceMappingURL=Model.js.map