import { Userdetailsdto } from './../models/userdetailsdto';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { JWTHelper } from '../helpers/JTWHelper';
import { AuthService } from '../services/auth/AuthService';
import { User } from '../models/user';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

@Component({
  selector: 'app-users-page',
  templateUrl: './users-page.component.html',
  styleUrls: ['./users-page.component.css']
})
export class UsersPageComponent implements OnInit {

  public user: User;
  public u: Userdetailsdto;
  fileUploadProgress: string = null;
  frmSaveUserDetails: FormGroup;

  constructor(private userService: AuthService, private formBuilder: FormBuilder, private cd: ChangeDetectorRef, private router: Router) { }

  ngOnInit() {
    if (sessionStorage.getItem('token') !== null) {
      const user = JWTHelper.decodedToken(sessionStorage.getItem('token'));
    }
    this.user = this.userService.user;
    this.u = new Userdetailsdto();
    this.validation();
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
    const form = this.frmSaveUserDetails.value;
    this.u = this.frmSaveUserDetails.value;
    this.u.email = this.user.email;

    this.userService.saveUserDetails(this.u)
    .subscribe(
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
}
