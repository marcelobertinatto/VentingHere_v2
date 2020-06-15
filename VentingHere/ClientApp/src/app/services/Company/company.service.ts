import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { of, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  private baseURL = 'http://localhost:56590/';

constructor(private http: HttpClient) { }

public getCompany(term: string): Observable<any> {
  if (term === '') {
    return;
  }
  return this.http.get<any>(this.baseURL + 'api/company/getcompany/' + term);
}

}
