import { JwtHelperService } from '@auth0/angular-jwt';

export class JWTHelper {

    static decodedToken(token: string) {
        const jwtHelper = new JwtHelperService();
        return jwtHelper.decodeToken(token);
    }
}