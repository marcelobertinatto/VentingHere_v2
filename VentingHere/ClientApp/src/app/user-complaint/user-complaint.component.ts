import { CompanyService } from './../services/Company/company.service';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import {debounceTime, distinctUntilChanged, map, switchMap, filter} from 'rxjs/operators';
import { of, Observable, fromEvent } from 'rxjs';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';

@Component({
  selector: 'app-user-complaint',
  templateUrl: './user-complaint.component.html',
  styleUrls: ['./user-complaint.component.css']
})

export class UserComplaintComponent implements OnInit {

  @ViewChild('searchInput', {static: false}) searchInput: ElementRef;

  frmSaveUserComplaint: FormGroup;
  company: string[];
  apiResponse: any;


  constructor(private companyService: CompanyService, private router: Router, private formBuilder: FormBuilder) { }

  ngAfterViewInit(): void {
    fromEvent<KeyboardEvent>(this.searchInput.nativeElement, 'keyup').pipe(
      // get value
      map((event: any) => {
        return event.target.value;
      })
      // if character length greater then 2
      , filter(res => res.length > 2)
      // Time in milliseconds between key events
      , debounceTime(1000)
      // If previous query is diffent from current
      , distinctUntilChanged()
      // subscription for response
    ).subscribe((text: string) => {
      this.search(text).subscribe((res) => {
        const data = Object.keys(res);
        const returnedValue = res[data[2]] as string[];
        if (returnedValue.length > 0) {
          this.apiResponse = res;
          this.company = returnedValue;
          const count = returnedValue.length;
        }
      }, (err) => {
        console.log('error', err);
      });
    });
  }


  ngOnInit() {
    this.validation();
  }

  validation() {
    this.frmSaveUserComplaint = this.formBuilder.group({
      companySearch: new FormControl(''),
      name: new FormControl(''),
      email: new FormControl(''),
      website: new FormControl('')
    });
  }

  search(term: string) {
    if (term === '') {
      return of([]);
    }
    const result = this.companyService.getCompany(term);
    return result;
   }
}
