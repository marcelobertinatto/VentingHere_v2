import { CompanyService } from './../services/Company/company.service';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import {debounceTime, distinctUntilChanged, map, switchMap, filter} from 'rxjs/operators';
import { of, Observable, fromEvent } from 'rxjs';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Company } from '../models/company';
import { Subject } from '../models/Subject';
import { SubjectIssue } from '../models/SubjectIssue';
import Swal from 'sweetalert2';
import { Companysubjecttellus } from '../models/companysubjecttellus';
import { JWTHelper } from '../helpers/JTWHelper';
import { AuthService } from '../services/auth/AuthService';
import { User } from '../models/user';

@Component({
  selector: 'app-user-complaint',
  templateUrl: './user-complaint.component.html',
  styleUrls: ['./user-complaint.component.css']
})

export class UserComplaintComponent implements OnInit {

  @ViewChild('searchInput', {static: false}) searchInput: ElementRef;

  frmSaveUserComplaint: FormGroup;
  public company: Company;
  private saveCompanySubjectTellUs: Companysubjecttellus;
  private companyId: number;
  private subjectId: number;
  private subjectIssueId: number;
  tellUs: boolean;
  apiResponse: any;
  isLoadingResult: boolean;
  keyword = 'companyName';
  notFound: string;
  public subjectItems: Subject;
  public subjectIssueItems: SubjectIssue;
  notFindSubject: boolean

  constructor(private companyService: CompanyService, private router: Router, private formBuilder: FormBuilder, private userService: AuthService) { }

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
        _this.isLoadingResult = false;
      }
      else {
        _this.notFound = "Not Found";
        _this.isLoadingResult = false;
        _this.frmSaveUserComplaint.patchValue({companyName: searchText});
      }
    }, (err) => {
      console.log('error', err);
    });    
  }
  
  ngOnInit() {
    const _this = this;
    this.saveCompanySubjectTellUs = new Companysubjecttellus();
    this.validation();
    
    this.companyService.getSubject().subscribe(
      (res) => {
        const data = Object.keys(res);
        const returnedValue = res[data[2]] as Subject;
        _this.subjectItems = returnedValue;
      }
    );
  }

  searchCleared() {
    this.company = null;
    this.notFound = null;
    this.notFindSubject = null;
    //this.frmSaveUserComplaint.reset();
    this.clearField();
    this.validation();
  }

  selectEvent(item) {
    this.notFound = null;
    this.frmSaveUserComplaint.patchValue({companyName: item.companyName, websiteaddress: item.webSiteAddress, address: item.address});
    this.companyId = item.id;
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
      companyName: [null, Validators.compose([Validators.required])],
      address: [null, Validators.compose([Validators.required])],
      websiteaddress: [null, Validators.compose([Validators.required])],
      subject: [null, Validators.compose([Validators.required])],
      subjectissue: [null, Validators.compose([Validators.required])],
      subjectdescribed: ['', Validators.compose([Validators.required])],
      subjectissuedescribed: ['', Validators.compose([Validators.required])],
      tellus: [null, Validators.compose([Validators.required])]
    });
  }

  clearField() {
    this.frmSaveUserComplaint.patchValue({name: '', website: '', address: '', websiteaddress: '', subject: '', subjectissue: '', subjectdescribed: '', tellus: ''});
  }

  save() {

    if(this.frmSaveUserComplaint.valid && this.userService.user !== null && this.userService.user !== undefined) {
      this.saveCompanySubjectTellUs = this.frmSaveUserComplaint.value;
      this.saveCompanySubjectTellUs.userId = this.userService.user.id;
      this.saveCompanySubjectTellUs.companyid = this.companyId;
      this.saveCompanySubjectTellUs.subjectId = this.subjectId;
      this.saveCompanySubjectTellUs.subjectIssueId = this.subjectIssueId;

      this.companyService.saveCompanySubjectTellUs(this.saveCompanySubjectTellUs).subscribe(
        data => {
          const res = Object.keys(data);
          const returnedValue = data[res[1]];
          const id = data[res[0]] as number;
          if (id === 2) {
            Swal.fire({
              position: 'top-end',
              title: 'Nicee...',
              text: returnedValue,
              icon: 'success',
              timer: 3500
            }).then(() => {
              this.router.navigate(['/userspage']);
            });
          } else {            
              Swal.fire({
                position: 'top-end',
                icon: 'error',
                text: returnedValue,
                timer: 3500
              });            
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

   changeSubject(e) {
     const subjectId = e;
     const _this = this;

    if(subjectId === '1') {
      this.notFindSubject = true;
      this.frmSaveUserComplaint.patchValue({subjectdescribed: '', subjectissuedescribed: '', tellus: '', subjectissue: 'a'});
    } else {
      this.subjectId = subjectId;
      this.companyService.getSubjectIssue(subjectId).subscribe(
        res => {
         const data = Object.keys(res);
         const returnedValue = res[data[2]] as SubjectIssue;
         _this.subjectIssueItems = returnedValue;
         this.notFindSubject = false;
        }
      )
    }
   }

   changeSubjectIssue(e) {
    this.subjectIssueId = e;
    this.frmSaveUserComplaint.patchValue({subjectdescribed: 'a', subjectissuedescribed: 'a'});
    this.tellUs = true;
   }
}
