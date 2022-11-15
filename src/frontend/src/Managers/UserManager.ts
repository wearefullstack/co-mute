import jwt_decode from "jwt-decode";

export interface IUser{
    id: string,
    name: string,
    surname: string,
    phone?: string,
    email: string,
    date_created?: string
}

export default
class UserManager {
    public static TOKEN_KEY: string = "active.user.token";
    private static INSTANCE: UserManager;
    private activeToken: string | null;

    constructor(){
        this.activeToken = this.getActiveToken();
    }



    getActiveToken(): string| null{
        return this.activeToken || (this.activeToken = localStorage.getItem(UserManager.TOKEN_KEY));
    }

    getActiveUser(): IUser | null{
        const token = this.getActiveToken();
        if(token){
            try {
                return jwt_decode<IUser>(token)
            } catch (error) {}
        }
        return null;
    }

    setActiveUser(token: string): IUser | null {
        localStorage.setItem(UserManager.TOKEN_KEY, token);
        try {
            return jwt_decode<IUser>(token);
        } catch (error) {
            return null;
        }
    }

    removeActiveUser(){
        localStorage.removeItem(UserManager.TOKEN_KEY);
        this.activeToken = null;
    }

    static getInstance(){
        return UserManager.INSTANCE || (UserManager.INSTANCE = new UserManager());
    }
}