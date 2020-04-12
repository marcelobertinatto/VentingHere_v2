import { HttpClient } from '@angular/common/http';

export class AuthService {

    constructor(private http: HttpClient) {
    }

    login(email: string, password: string) {
        if (email === 'm@hotmail.com' && password === '123456') {
            return true;
        }
        return false;
    }
}
