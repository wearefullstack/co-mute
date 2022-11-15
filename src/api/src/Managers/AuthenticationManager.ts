import JWT from 'jsonwebtoken';
import APIError from '../APIError';


export default 
class JWTManager {
    private static INSTANCE: JWTManager;
    private static DEFAULT_SIGNING_KEY = "jwiu21ywhkHWU8w109w";

    constructor(private signingKey: string){}

    async sign<TPayload extends object>(payload: TPayload){
        try {
            return JWT.sign({...payload}, this.signingKey)
        } catch (error) {
            return Promise.reject(APIError.eServer("JWT Manager").log(error));
        }
    }

    async verify(token: string){
        try {
            const payload = JWT.verify(token, this.signingKey);
            delete (payload as any).iat;
            return payload;
        } catch (error) {
            return Promise.reject(APIError.eServer("JWT Manager").log(error))
        }
    }

    static getInstance(){
        const {} = process.env;
        return JWTManager.INSTANCE || ( JWTManager.INSTANCE = new JWTManager(process.env.JWT_SIGNING_KEY || JWTManager.DEFAULT_SIGNING_KEY));
    }
} 