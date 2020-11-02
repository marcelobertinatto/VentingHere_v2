import { Component, OnInit } from '@angular/core';
import { Company } from '../models/company';
import { CompanyService } from '../services/Company/company.service';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home-banner',
  templateUrl: './home-banner.component.html',
  styleUrls: ['./home-banner.component.css']
})
export class HomeBannerComponent implements OnInit {

  frmUserSearch: FormGroup;
  public company: Company;
  apiResponse: any;
  isLoadingResult: boolean;
  keyword = 'companyName';
  notFound: string;

  constructor(private companyService: CompanyService,private formBuilder: FormBuilder, private router: Router) { }

  ngOnInit() {
    this.validation();
  }  

  validation() {
    this.frmUserSearch = this.formBuilder.group({
      companyName: [null, Validators.compose([Validators.required])]
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
        _this.frmUserSearch.patchValue({companyName: searchText});
      }
    }, (err) => {
      console.log('error', err);
    });    
  }

  selectEvent(item) {
    this.notFound = null;
    this.router.navigateByUrl('/companydetails/', { state: item });
    //this.frmSaveUserComplaint.patchValue({companyName: item.companyName, websiteaddress: item.webSiteAddress, address: item.address});
    //this.companyId = item.id;
  }

  onChangeSearch(val: string) {
    this.getServerResponse(val);
  }

  onFocused(e) {
    // do something when input is focused
  }

  clearField() {
    this.frmUserSearch.patchValue({name: ''});
  }

  searchCleared() {
    this.company = null;
    this.notFound = null;
    this.clearField();
    this.validation();
  }

}
