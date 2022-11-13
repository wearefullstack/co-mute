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
        MySQLManager_1.default.getInstance()
            .withTransaction((connection, Query) => __awaiter(this, void 0, void 0, function* () {
            if (saveMode === "Create") {
                const args = [];
                const keys = Object.keys(this.src);
                keys.forEach(key => {
                    args.push(this.src[key]);
                });
                const cols = `(${keys.join(",")})`;
                const query = `INSERT INTO ${this.name} ${cols} VALUES (?)`;
                return yield Query(query, [args]);
            }
            else {
                const clone = Object.assign({}, this.src);
                delete clone.id;
                const query = `UPDATE ${this.name} SET ? WHERE id=?`;
                return yield Query(query, [clone, this.src.id]);
            }
        }));
    }
}
exports.default = Model;
//# sourceMappingURL=Modle.js.map