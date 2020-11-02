import { Userdetailsdto } from './../../models/userdetailsdto';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Userlogindto } from 'src/app/models/userlogindto';
import { User } from 'src/app/models/user';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { JWTHelper } from 'src/app/helpers/JTWHelper';

@Injectable({
    providedIn: 'root'
  })
export class AuthService {

    private baseURL = 'http://localhost:56590/';
    public _user: User;

    constructor(private http: HttpClient) {
    }

    get headers(): HttpHeaders {
        return new HttpHeaders({
          'Content-Type': 'application/json'
        });
      }

      get user(): User {
        const user_json = sessionStorage.getItem('user-authenticated');
        const u = JWTHelper.decodedToken(sessionStorage.getItem('token'));
        this._user = JSON.parse(user_json);
        if (u != null) {
          this._user.id = u.nameid;
        }
        return this._user;
      }

      set user(user: User) {
        sessionStorage.setItem('user-authenticated', JSON.stringify(user));
        this._user = user;
      }

    login(u: Userlogindto) {
        return this.http.post<User>(this.baseURL + 'api/user/login', JSON.stringify(u), {headers: this.headers});
    }

    facebooklogin(u: Userlogindto) {
      return this.http.post<User>(this.baseURL + 'api/user/facebooklogin', JSON.stringify(u), {headers: this.headers});
    } 

    public registerUser(u: User): Observable<User> {
      return this.http.post<User>(this.baseURL + 'api/user/registeruser', JSON.stringify(u), {headers: this.headers});
    }

    public facebookregisterUser(u: User): Observable<User> {
      return this.http.post<User>(this.baseURL + 'api/user/facebookregisteruser', JSON.stringify(u), {headers: this.headers});
    }

    public saveUserDetails(u: Userdetailsdto): Observable<User> {
      return this.http.post<User>(this.baseURL + 'api/user/saveuserdetails', JSON.stringify(u), {headers: this.headers});
    }

    public user_Authenticated(): boolean {
      let ret = false;
      if (this.user != null && (this._user.email !== '' || this._user.email !== null) &&
      (this._user.password !== '' || this._user.password !== null)) {
        ret = true;
      }
      return ret;
    }

    public session_cleaner() {
      sessionStorage.clear();
      this._user = null;
    }
}
