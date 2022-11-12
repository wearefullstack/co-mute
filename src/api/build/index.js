"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const dotenv_1 = __importDefault(require("dotenv"));
const MySQLManager_1 = __importDefault(require("./Managers/MySQLManager"));
dotenv_1.default.config();
MySQLManager_1.default.init();
//# sourceMappingURL=index.js.map