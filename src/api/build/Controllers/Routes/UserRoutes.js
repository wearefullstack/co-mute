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
exports.UpdateUserRoute = exports.LoginUserRoute = exports.RegisterUserRoute = void 0;
const express_validator_1 = require("express-validator");
const APIError_1 = __importDefault(require("../../APIError"));
const AuthenticationManager_1 = __importDefault(require("../../Managers/AuthenticationManager"));
const User_1 = __importDefault(require("../../Models/User"));
const Route_1 = __importDefault(require("./Route"));
class RegisterUserRoute extends Route_1.default {
    constructor() {
        super(...arguments);
        this.path = "/register";
        this.validator = [
            express_validator_1.body("name").isString().isLength({ max: 32 }),
            express_validator_1.body("surname").isString().isLength({ max: 32 }),
            express_validator_1.body("phone").optional({ checkFalsy: true }).isString().isLength({ max: 24 }),
            express_validator_1.body("email").isString().isEmail().isLength({ max: 32 }),
            express_validator_1.body("password").isString()
        ];
    }
    handle(request) {
        return __awaiter(this, void 0, void 0, function* () {
            const { name, surname, phone, email, password } = request.body;
            const user = yield User_1.default.register({ name, surname, phone, email }, password);
            try {
                return AuthenticationManager_1.default.getInstance().sign(user);
            }
            catch (error) {
                return Promise.reject(APIError_1.default.eServer("UserRoute").log(error));
            }
        });
    }
}
exports.RegisterUserRoute = RegisterUserRoute;
class LoginUserRoute extends Route_1.default {
    constructor() {
        super(...arguments);
        this.path = "/login";
        this.validator = [
            express_validator_1.body("email").isString().isEmail().isLength({ max: 32 }),
            express_validator_1.body("password").isString()
        ];
    }
    handle(request) {
        return __awaiter(this, void 0, void 0, function* () {
            const { email, password } = request.body;
            const user = yield User_1.default.loginUser(email, password);
            try {
                return AuthenticationManager_1.default.getInstance().sign(user);
            }
            catch (error) {
                return Promise.reject(APIError_1.default.eServer("UserRoute").log(error));
            }
        });
    }
}
exports.LoginUserRoute = LoginUserRoute;
class UpdateUserRoute extends Route_1.default {
    constructor() {
        super(...arguments);
        this.path = "/update";
        this.validator = [
            express_validator_1.body("name").isString().isLength({ max: 32 }),
            express_validator_1.body("surname").isString().isLength({ max: 32 }),
            express_validator_1.body("phone").optional({ checkFalsy: true }).isString().isLength({ max: 24 }),
        ];
    }
    handle(request) {
        return __awaiter(this, void 0, void 0, function* () {
            const { name, surname, phone } = request.body;
            const authentication = request.authentication;
            const user = yield (new User_1.default(Object.assign(Object.assign({}, authentication), { name, surname, phone }))).updateUser();
            try {
                return AuthenticationManager_1.default.getInstance().sign(user);
            }
            catch (error) {
                return Promise.reject(APIError_1.default.eServer("UserRoute").log(error));
            }
        });
    }
}
exports.UpdateUserRoute = UpdateUserRoute;
//# sourceMappingURL=UserRoutes.js.map