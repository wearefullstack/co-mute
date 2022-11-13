import Model from "./Model";
export interface IUser {
    id: string;
    name: string;
    surname: string;
    phone?: string;
    email: string;
    password_hash: string;
    password_salt: number;
    date_created?: Date;
}
export default class User extends Model<IUser> {
    static SALT_ROUNDS: number;
    constructor(src: IUser);
    static loginUser(email: string, password: string): Promise<IUser>;
    static register(src: Omit<IUser, "id" | "date_created" | "password_hash" | "password_salt">, password: string): Promise<IUser>;
    updateUser(): Promise<IUser>;
}
//# sourceMappingURL=User.d.ts.map