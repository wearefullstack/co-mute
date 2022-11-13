import JWT from 'jsonwebtoken';
export default class JWTManager {
    private signingKey;
    private static INSTANCE;
    private static DEFAULT_SIGNING_KEY;
    constructor(signingKey: string);
    sign<TPayload extends object>(payload: TPayload): Promise<string>;
    verify(token: string): Promise<string | JWT.JwtPayload>;
    static getInstance(): JWTManager;
}
//# sourceMappingURL=AuthenticationManager.d.ts.map