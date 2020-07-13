import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { of, Observable } from 'rxjs';
import { Company } from 'src/app/models/company';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  private baseURL = 'http://localhost:56590/';

constructor(private http: HttpClient) { }

get headers(): HttpHeaders {
  return new HttpHeaders({
    'Content-Type': 'application/json'
  });
}

public getCompany(term: string) {
  if (term === '') {
    return of([]);
  }
  return this.http.get(this.baseURL + 'api/company/getcompany/' + term);
}

public saveCompany(co: Company) : Observable<Company> {
  return this.http.post<Company>(this.baseURL + 'api/company/savecompany', JSON.stringify(co), {headers: this.headers});
}

}
