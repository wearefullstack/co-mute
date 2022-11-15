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
const jsonwebtoken_1 = __importDefault(require("jsonwebtoken"));
const APIError_1 = __importDefault(require("../APIError"));
class JWTManager {
    constructor(signingKey) {
        this.signingKey = signingKey;
    }
    sign(payload) {
        return __awaiter(this, void 0, void 0, function* () {
            try {
                return jsonwebtoken_1.default.sign(Object.assign({}, payload), this.signingKey);
            }
            catch (error) {
                return Promise.reject(APIError_1.default.eServer("JWT Manager").log(error));
            }
        });
    }
    verify(token) {
        return __awaiter(this, void 0, void 0, function* () {
            try {
                const payload = jsonwebtoken_1.default.verify(token, this.signingKey);
                delete payload.iat;
                return payload;
            }
            catch (error) {
                return Promise.reject(APIError_1.default.eServer("JWT Manager").log(error));
            }
        });
    }
    static getInstance() {
        const {} = process.env;
        return JWTManager.INSTANCE || (JWTManager.INSTANCE = new JWTManager(process.env.JWT_SIGNING_KEY || JWTManager.DEFAULT_SIGNING_KEY));
    }
}
exports.default = JWTManager;
JWTManager.DEFAULT_SIGNING_KEY = "jwiu21ywhkHWU8w109w";
//# sourceMappingURL=AuthenticationManager.js.map