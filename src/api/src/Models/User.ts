import APIError from "../APIError";
import MySQLManager from "../Managers/MySQLManager";
import Model from "./Model";
import { v4 as uuidV4 } from 'uuid';
import bcrypt from 'bcrypt';

export interface IUser {
    id: string,
    name: string,
    surname: string,
    phone?: string,
    email: string,
    password_hash: string,
    password_salt: number,
    date_created?: Date
}

export default
class User extends Model<IUser> {
    public static SALT_ROUNDS = 9;

    constructor(src: IUser){
        super("users", src);
    }

    static loginUser(email: string, password: string){
        return MySQLManager.getInstance()
        .withTransaction<IUser>(async (connection, queryExecutor) => {
            const user = await Model.findOne<IUser>(email, "users", "email");

            if(user){
                const passwordsMatch = await bcrypt.compare(password, user.password_hash);
                return passwordsMatch ? user : Promise.reject(APIError.eForbidden("User").toError());
            }else{
                return Promise.reject(APIError.eForbidden("User").toError())
            }
        });
    }

    static register(src: Omit<IUser, "id" | "date_created" | "password_hash" | "password_salt">, password: string){
        return MySQLManager.getInstance()
        .withTransaction<IUser>(async (connection, queryExecutor) => {
            const rawUser = await Model.findOne<IUser>(src.email, "users", "email");
            
            if(!rawUser){
                const id: string = uuidV4();
                const password_hash: string = await bcrypt.hash(password, User.SALT_ROUNDS);

                const user: User = new User({ id, password_hash, password_salt: User.SALT_ROUNDS, ...src, date_created: new Date()});
                await user.save("Create");
                return user.src;
            }else{
                return Promise.reject(APIError.eForbidden("User", "User Already Exists", "0x0").toError());
            }

        });
    }

    updateUser(){
        return MySQLManager.getInstance()
        .withTransaction<IUser>((connection, queryExecutor) => {
            return this.save("Update");
        });
    }

    

   
}