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
const APIError_1 = __importDefault(require("../APIError"));
const MySQLManager_1 = __importDefault(require("../Managers/MySQLManager"));
const Model_1 = __importDefault(require("./Model"));
const uuid_1 = require("uuid");
const bcrypt_1 = __importDefault(require("bcrypt"));
class User extends Model_1.default {
    constructor(src) {
        super("users", src);
    }
    static loginUser(email, password) {
        return MySQLManager_1.default.getInstance()
            .withTransaction((connection, queryExecutor) => __awaiter(this, void 0, void 0, function* () {
            const user = yield Model_1.default.findOne(email, "users", "email");
            if (user) {
                const passwordsMatch = yield bcrypt_1.default.compare(password, user.password_hash);
                return passwordsMatch ? user : Promise.reject(APIError_1.default.eForbidden("User").toError());
            }
            else {
                return Promise.reject(APIError_1.default.eForbidden("User").toError());
            }
        }));
    }
    static register(src, password) {
        return MySQLManager_1.default.getInstance()
            .withTransaction((connection, queryExecutor) => __awaiter(this, void 0, void 0, function* () {
            const rawUser = yield Model_1.default.findOne(src.email, "users", "email");
            if (!rawUser) {
                const id = uuid_1.v4();
                const password_hash = yield bcrypt_1.default.hash(password, User.SALT_ROUNDS);
                const user = new User(Object.assign({ id, password_hash, password_salt: User.SALT_ROUNDS }, src));
                yield user.save("Create");
                return user.src;
            }
            else {
                return Promise.reject(APIError_1.default.eForbidden("User", "User Already Exists", "0x0").toError());
            }
        }));
    }
    updateUser() {
        return MySQLManager_1.default.getInstance()
            .withTransaction((connection, queryExecutor) => {
            return this.save("Update");
        });
    }
}
exports.default = User;
User.SALT_ROUNDS = 9;
//# sourceMappingURL=User.js.map