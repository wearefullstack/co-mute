"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_validator_1 = require("express-validator");
const APIError_1 = __importDefault(require("../APIError"));
const AuthenticationManager_1 = __importDefault(require("../Managers/AuthenticationManager"));
class Controller {
    control(routePath, authenticate = false) {
        const route = this.routes[routePath];
        const reqHander = (req, res) => {
            const errors = express_validator_1.validationResult(req);
            if (errors.isEmpty())
                route.handle(req)
                    .then(result => {
                    res.status(200).json({ result });
                }).
                    catch(error => {
                    res.status(error.status_code).json({ error });
                    //console.log(error);
                    console.log(2);
                })
                    .catch(error => { console.log(1); });
            else
                res.json({ error: errors.array() });
        };
        const handlers = authenticate ? [...route.validator, this.authenticate, reqHander] : [...route.validator, reqHander];
        return {
            path: this.path + route.path,
            handlers
        };
    }
    authenticate(req, res, next) {
        const { authorization } = req.headers;
        if (authorization) {
            const token = authorization.split(" ")[1];
            if (token) {
                AuthenticationManager_1.default.getInstance().verify(token)
                    .then(payload => {
                    req.authentication = payload;
                    next();
                })
                    .catch(_ => {
                    const error = APIError_1.default.eServer("Contoller").toError();
                    res.status(error.status_code).json({ error });
                });
                return;
            }
        }
        const error = APIError_1.default.eUnauthorized("Controller").toError();
        res.status(error.status_code).json({ error });
    }
}
exports.default = Controller;
//# sourceMappingURL=Controller.js.map