import { Component, OnInit } from '@angular/core';
import { Companysubjecttellus } from '../models/companysubjecttellus';
import { User } from '../models/user';
import { AuthService } from '../services/auth/AuthService';

@Component({
  selector: 'app-complaint-details',
  templateUrl: './complaint-details.component.html',
  styleUrls: ['./complaint-details.component.css']
})
export class ComplaintDetailsComponent implements OnInit {

  complaintDetails: Companysubjecttellus;
  public user: User;

  constructor(private userService: AuthService) { }

  ngOnInit() {
    this.complaintDetails = history.state;
    this.user = this.userService.user;
  }

  approve(item: number){
    window.alert('Id: ' + item);
  }

}
