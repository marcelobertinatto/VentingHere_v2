import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { of, Observable } from 'rxjs';
import { Company } from 'src/app/models/company';
import { Subject } from 'src/app/models/subject';
import { SubjectIssue } from 'src/app/models/subjectIssue';
import { map } from 'rxjs/operators';
import { Companysubjecttellus } from 'src/app/models/companysubjecttellus';

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

public saveCompanySubjectTellUs(co: Companysubjecttellus) : Observable<Companysubjecttellus> {
  return this.http.post<Companysubjecttellus>(this.baseURL + 'api/company/savecompanytellus', JSON.stringify(co), {headers: this.headers});
}

public getSubject() {
  //return this.http.get(this.baseURL + 'api/company/getsubject');
  return this.http.get(this.baseURL + 'api/company/getsubject').pipe(
    map((data: Subject[]) => {
      return data;
    })
  );
}

public getSubjectIssue(id: string) {
  if(id === undefined || id === '' || id === null) {
    return;
  }

  return this.http.get(this.baseURL + 'api/company/getsubjectissue/' + id).pipe(
    map((data: SubjectIssue) => {
      return data;
    })
  );
}

}
