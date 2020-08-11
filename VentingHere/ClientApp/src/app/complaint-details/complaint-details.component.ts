import { Component, OnInit } from '@angular/core';
import { Companysubjecttellus } from '../models/companysubjecttellus';

@Component({
  selector: 'app-complaint-details',
  templateUrl: './complaint-details.component.html',
  styleUrls: ['./complaint-details.component.css']
})
export class ComplaintDetailsComponent implements OnInit {

  complaintDetails: Companysubjecttellus;

  constructor() { }

  ngOnInit() {
    this.complaintDetails = history.state.id;
  }

}
