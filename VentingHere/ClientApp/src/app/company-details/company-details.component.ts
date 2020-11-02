import { Component, OnInit } from '@angular/core';
import { Company } from '../models/company';

@Component({
  selector: 'app-company-details',
  templateUrl: './company-details.component.html',
  styleUrls: ['./company-details.component.css']
})
export class CompanyDetailsComponent implements OnInit {

  companyDetails: Company;

  constructor() { }

  ngOnInit() {
    this.companyDetails = history.state;
  }

}
