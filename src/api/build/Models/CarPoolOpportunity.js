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
const Model_1 = __importDefault(require("./Model"));
const moment_1 = __importDefault(require("moment"));
const APIError_1 = __importDefault(require("../APIError"));
const uuid_1 = require("uuid");
const MySQLManager_1 = __importDefault(require("../Managers/MySQLManager"));
class CarPoolOpportunities extends Model_1.default {
    constructor(src) {
        super(CarPoolOpportunities.TABLE_NAME, src);
    }
    static Create(src, owner) {
        return __awaiter(this, void 0, void 0, function* () {
            return MySQLManager_1.default.getInstance()
                .withTransaction((connection, Query) => __awaiter(this, void 0, void 0, function* () {
                const mDepTime = moment_1.default(src.departure_time, "HH:mm:ss", false), mEATTime = moment_1.default(src.expected_arrival_time, "HH:mm:ss", false);
                if (mDepTime.isBefore(mEATTime)) {
                    const days = src.days_available.split(",");
                    if (!(yield this.hasOverlappingCPOs(src.departure_time, src.expected_arrival_time, days, owner))) {
                        const id = uuid_1.v4();
                        const CPO = Object.assign({ id, owner }, src);
                        (new CarPoolOpportunities(CPO)).save("Create");
                        return CPO;
                    }
                    else {
                        return Promise.reject(APIError_1.default.eForbidden("This Car Pools time range overlapps with one of your created/join Car Pools", "0x1").toError());
                    }
                }
                else {
                    return Promise.reject(APIError_1.default.eForbidden("Departure Time cannot be after Expected Arrival Time.", '0x0').toError());
                }
            }));
        });
    }
    static hasOverlappingCPOs(departure_time, expected_arrival_time, days, owner) {
        return __awaiter(this, void 0, void 0, function* () {
            return (yield this.hasOverlayingCreatedCPOs(departure_time, expected_arrival_time, days, owner)) || (yield this.hasOverlayingJoinedCPOs(departure_time, expected_arrival_time, days, owner));
        });
    }
    static hasOverlayingCreatedCPOs(departure_time, expected_arrival_time, days, owner) {
        return __awaiter(this, void 0, void 0, function* () {
            const args = [departure_time, departure_time, expected_arrival_time, expected_arrival_time, departure_time, expected_arrival_time, departure_time, expected_arrival_time, owner];
            const whereCondition = `(${CarPoolOpportunities.createIsOverlayingCondition()}) AND owner=?;`;
            const overlappingCPOs = yield Model_1.default.find(CarPoolOpportunities.TABLE_NAME, whereCondition, args);
            if (overlappingCPOs.length > 0) {
                return overlappingCPOs.some(overlappingCPO => {
                    return this.hasOverlappingDays(days, overlappingCPO.days_available.split(','));
                });
            }
            else {
                return false;
            }
        });
    }
    static hasOverlappingDays(days0, days1) {
        return days0.some(day0 => {
            const stdDay0 = day0.trim().toLowerCase();
            return days1.some(day1 => {
                const stdDay1 = day1.trim().toLowerCase();
                return stdDay0 == stdDay1;
            });
        });
    }
    static hasOverlayingJoinedCPOs(departure_time, expected_arrival_time, days, user) {
        return __awaiter(this, void 0, void 0, function* () {
            const args = [user, departure_time, departure_time, expected_arrival_time, expected_arrival_time, departure_time, expected_arrival_time, departure_time, expected_arrival_time];
            const tables = 'car_pool_opportunity, users_has_car_pool_opportunity as JCPO';
            const whereCondition = `JCPO.car_pool_opportunity_id=id AND JCPO.users_id=? AND (${this.createIsOverlayingCondition()})`;
            const overlappingCPOs = yield Model_1.default.find(tables, whereCondition, args);
            if (overlappingCPOs.length > 0) {
                return overlappingCPOs.some(overlappingCPO => {
                    return this.hasOverlappingDays(days, overlappingCPO.on_which_days.split(','));
                });
            }
            else {
                return false;
            }
        });
    }
    static createIsOverlayingCondition() {
        const DTWithin = `departure_time <  ? AND expected_arrival_time > ?`;
        const EATWithin = `departure_time < ? AND expected_arrival_time > ?`;
        const _DTWithin = `? < departure_time AND ? > departure_time`;
        const _EATWithin = `? < expected_arrival_time AND ? > expected_arrival_time`;
        return `${DTWithin} OR ${EATWithin} OR ${_DTWithin} OR ${_EATWithin}`;
    }
}
exports.default = CarPoolOpportunities;
CarPoolOpportunities.TABLE_NAME = "car_pool_opportunity";
//# sourceMappingURL=CarPoolOpportunity.js.map