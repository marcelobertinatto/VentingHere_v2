import { CompanyService } from './../services/Company/company.service';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import {debounceTime, distinctUntilChanged, map, switchMap, filter} from 'rxjs/operators';
import { of, Observable, fromEvent } from 'rxjs';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { Company } from '../models/company';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-user-complaint',
  templateUrl: './user-complaint.component.html',
  styleUrls: ['./user-complaint.component.css']
})

export class UserComplaintComponent implements OnInit {

  @ViewChild('searchInput', {static: false}) searchInput: ElementRef;

  frmSaveUserComplaint: FormGroup;
  public company: Company;
  private saveCo: Company;
  apiResponse: any;
  isLoadingResult: boolean;
  keyword = 'companyName';
  errorMsg: string;

  constructor(private companyService: CompanyService, private router: Router, private formBuilder: FormBuilder) { }

  ngAfterViewInit(): void {
    const _this = this;
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
        const returnedValue = res[data[2]] as Company;
        // if (returnedValue.length > 0) {
        if (returnedValue !== undefined || returnedValue !== null) {
          _this.apiResponse = res;
          _this.company = returnedValue;
          //const count = returnedValue.length;
        }
      }, (err) => {
        console.log('error', err);
      });
    });
  }

  getServerResponse(searchText) {
    this.clearField();
    this.isLoadingResult = true;

    const _this = this;
    this.companyService.getCompany(searchText).subscribe((res) => {
      const data = Object.keys(res);
      const returnedValue = res[data[2]] as Company;
      if (returnedValue !== undefined && returnedValue !== null) {
        _this.company = returnedValue;
        this.isLoadingResult = false;
      }
      else {
        _this.errorMsg = "Not Found";
        this.isLoadingResult = false;
        this.frmSaveUserComplaint.patchValue({companyName: searchText});
      }
    }, (err) => {
      console.log('error', err);
    });    
  }
  
  ngOnInit() {
    this.validation();
  }

  searchCleared() {
    this.company = null;
    this.errorMsg = null;
  }

  selectEvent(item) {
    this.errorMsg = null;
    this.frmSaveUserComplaint.patchValue({name: item.companyName, website: item.webSiteAddress, address: item.address});
  }

  onChangeSearch(val: string) {
    this.getServerResponse(val);
  }

  onFocused(e) {
    // do something when input is focused
  }

  validation() {
    this.frmSaveUserComplaint = this.formBuilder.group({
      companySearch: new FormControl(''),
      companyName: new FormControl(''),
      address: new FormControl(''),
      websiteaddress: new FormControl('')
    });
  }

  clearField() {
    this.frmSaveUserComplaint.patchValue({name: '', website: '', address: ''});
  }

  save() {
    if(this.frmSaveUserComplaint.valid) {
      this.saveCo = this.frmSaveUserComplaint.value;

      this.companyService.saveCompany(this.saveCo).subscribe(
        data => {
          if (data['Success'] != null) {
            Swal.fire({
              position: 'top-end',
              title: 'Nicee...',
              text: data['Success'],
              icon: 'success',
              timer: 3500
            }).then(() => {
              this.router.navigate(['/']);
            });
          } else {
            if (data['Error'] != null) {
              Swal.fire({
                position: 'top-end',
                icon: 'error',
                text: data['Error'],
                timer: 3500
              });
            } else {
              Swal.fire({
                position: 'top-end',
                icon: 'error',
                text: data['InternalErrors'],
                timer: 3500
              });
            }
          }
        }, err => {
          Swal.fire({
            position: 'top-end',
            icon: 'error',
            text: err,
            timer: 3500
          });
        }
      )
    }
  }

  search(term: string) {
    if (term === '') {
      return of([]);
    }
    const result = this.companyService.getCompany(term);
    return result;
   }   
}
