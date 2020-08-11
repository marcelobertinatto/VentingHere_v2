import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Companysubjecttellus } from 'src/app/models/companysubjecttellus';
import { Observable } from 'rxjs';
import { Usersummary } from 'src/app/models/usersummary';

@Injectable({
  providedIn: 'root'
})

export class UserService {

  private baseURL = 'http://localhost:56590/';

constructor(private http: HttpClient) { }

get headers(): HttpHeaders {
  return new HttpHeaders({
    'Content-Type': 'application/json'
  });
}

public getuserscomplaint(userId: number) : Observable<Usersummary> {
  return this.http.post<Usersummary>(this.baseURL + 'api/user/getusercomplaints', JSON.stringify(userId), {headers: this.headers});
}

}
