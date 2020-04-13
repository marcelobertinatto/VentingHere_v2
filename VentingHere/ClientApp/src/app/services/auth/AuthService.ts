import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Userlogindto } from 'src/app/models/userlogindto';
import { User } from 'src/app/models/user';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
  })
export class AuthService {

    private baseURL = 'http://localhost:56590/';

    constructor(private http: HttpClient) {
    }

    get headers(): HttpHeaders {
        return new HttpHeaders({
          'Content-Type': 'application/json'
        });
      }

    login(u: Userlogindto) {
        return this.http.post<User>(this.baseURL + 'api/user/login', JSON.stringify(u), {headers: this.headers});
    }

    public registerUser(u: User): Observable<User> {
      return this.http.post<User>(this.baseURL + 'api/user/registeruser', JSON.stringify(u), {headers: this.headers});
    }
}
