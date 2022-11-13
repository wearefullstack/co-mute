import JWT from 'jsonwebtoken';


export default 
class JWTManager {
    private static INSTANCE: JWTManager;
    private static DEFAULT_SIGNING_KEY = "jwiu21ywhkHWU8w109w";

    constructor(private signingKey: string){}

    async sign<TPayload extends object>(payload: TPayload){
        console.log("::::PAYLOAD", {...payload});
        return JWT.sign({...payload}, this.signingKey)
    }

    async verify(token: string){
        const payload = JWT.verify(token, this.signingKey);
        delete (payload as any).iat;
        return payload;
    }

    static getInstance(){
        const {} = process.env;
        return JWTManager.INSTANCE || ( JWTManager.INSTANCE = new JWTManager(process.env.JWT_SIGNING_KEY || JWTManager.DEFAULT_SIGNING_KEY));
    }
} 