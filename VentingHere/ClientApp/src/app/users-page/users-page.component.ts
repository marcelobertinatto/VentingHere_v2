import { ComponentFixture } from '@angular/core/testing';
import { Userdetailsdto } from './../models/userdetailsdto';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { JWTHelper } from '../helpers/JTWHelper';
import { AuthService } from '../services/auth/AuthService';
import { User } from '../models/user';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';
import { UserService } from '../services/User/User.service';
import { Usersummary } from '../models/usersummary';
import { Companysubjecttellus } from '../models/companysubjecttellus';

@Component({
  selector: 'app-users-page',
  templateUrl: './users-page.component.html',
  styleUrls: ['./users-page.component.css']
})
export class UsersPageComponent implements OnInit {

  public user: User;
  public u: Userdetailsdto;
  public userSummary: Usersummary;
  fileUploadProgress: string = null;
  frmSaveUserDetails: FormGroup;
  enableUserSummary: boolean = false;

  constructor(private userService: AuthService, private usService: UserService,
    private formBuilder: FormBuilder, private cd: ChangeDetectorRef, private router: Router) { }

  ngOnInit() {
    if (sessionStorage.getItem('token') !== null) {
      const user = JWTHelper.decodedToken(sessionStorage.getItem('token'));
    }
    this.user = this.userService.user;
    this.u = new Userdetailsdto();
    this.validation();
    this.getusercomplaints(this.user.id);
  }

  // tslint:disable-next-line: use-lifecycle-interface
  ngAfterViewInit(): void {
    this.frmSaveUserDetails.patchValue({username: this.user.userName, fullname: this.user.name});
  }

  validation () {
    this.frmSaveUserDetails = this.formBuilder.group({
      imageFile: new FormControl(''),
      username: new FormControl(''),
      fullname: new FormControl(''),
      currentpassword: new FormControl(''),
      newpassword: new FormControl(''),
      confirmnewpassword: new FormControl('')
    });
  }

  public saveUserDetails() {
    this.u.fullname = this.frmSaveUserDetails.get('fullname').value;
    this.u.currentpassword = this.frmSaveUserDetails.get('currentpassword').value;
    this.u.newpassword = this.frmSaveUserDetails.get('newpassword').value;
    this.u.confirmnewpassword = this.frmSaveUserDetails.get('confirmnewpassword').value;
    this.u.username = this.frmSaveUserDetails.get('username').value;
    this.u.email = this.user.email;

    this.userService.saveUserDetails(this.u)
    .subscribe(
      data => {
        const d = Object.keys(data);
        const msgId = data[d[0]];
        const msg = data[d[1]];
        const returnedU = data[d[2]] as Userdetailsdto;
        if (returnedU != null && msgId === 2) {
          this.user.image = returnedU.userimage;
          this.user.name = returnedU.fullname;
          sessionStorage.setItem('user-authenticated', JSON.stringify(this.user));
          Swal.fire({
            position: 'top-end',
            title: 'Nicee...',
            text: msg,
            icon: 'success',
            timer: 3500
          }).then(() => {
            this.router.navigate(['/userspage']);
          });
        } else {
          if (data['Error'] != null) {
            Swal.fire({
              position: 'top-end',
              icon: 'error',
              text: msg,
              timer: 3500
            });
          } else {
            Swal.fire({
              position: 'top-end',
              icon: 'error',
              text: msg,
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
    );
  }

  onFileChange(event) {
    const fileData = <File>event.target.files[0];

    const reader = new FileReader();
     reader.onload = this._handleReaderLoaded.bind(this);
     reader.readAsBinaryString(fileData);
  }

  _handleReaderLoaded(readerEvt) {
    const binaryString = readerEvt.target.result;
    this.u.userimage = btoa(binaryString);  // Converting binary string data.
  }

  public logout() {
    this.userService.session_cleaner();
    this.router.navigate(['/']);
  }

  public enableComplaints(){
    if(this.userSummary !== undefined || this.userSummary !== null) {
      this.enableUserSummary = true;
    }
  }

  public getusercomplaints(id: number) {
    const _this = this;
    this.usService.getuserscomplaint(id).subscribe(
      data => {
        const d = Object.keys(data);
        const msgId = data[d[0]];
        const msg = data[d[1]];
        const returnedU = data[d[2]] as Usersummary;
        if (returnedU != null && msgId === 2) {
          _this.userSummary = returnedU;
        }
      }
    );
  }

  getcomplaintdetailspage(comp: Companysubjecttellus) { 
    this.router.navigateByUrl('/complaintdetails/', { state: {id: comp} });
  }

}
