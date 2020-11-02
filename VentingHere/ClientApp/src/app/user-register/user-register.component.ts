import { AuthService } from './../services/auth/AuthService';
import { Component, OnInit } from '@angular/core';
import { ModalService } from '../services/Modal.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CustomValidators } from '../helpers/customValidators';
import Swal from 'sweetalert2';
import { User } from '../models/user';
import { Router } from '@angular/router';
import { SocialAuthService, FacebookLoginProvider, SocialUser } from 'angularx-social-login';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit {

  registerform: FormGroup;
  public u: User;
  public message: string;
  userFB: SocialUser;

  constructor(private modal: ModalService, private formBuilder: FormBuilder, private authService: AuthService,
    private router: Router, private socialAuthService: SocialAuthService) { }

  ngOnInit() {
    this.u = new User();
    this.validation();
  }

  close() {
    this.modal.close();
  }

  validation() {
    this.registerform = this.formBuilder.group({
      email: [null, Validators.compose([Validators.required, Validators.email])],
      password: [null,
        Validators.compose([
          // 1. Password Field is Required
          Validators.required,
          // 2. check whether the entered password has a number
          CustomValidators.patternValidator(/\d/, { hasNumber: true }),
          // 3. At least one letter
          CustomValidators.patternValidator(/[a-zA-Z]/, { hasLetter: true }),
          Validators.minLength(6)])
      ],
      name: [null, Validators.compose([Validators.required])],
      userName: [null, Validators.compose([Validators.required])]
    });
  }

  public register() {

    if (this.registerform.valid) {
      this.u = this.registerform.value;
      this.authService.registerUser(this.u)
      .subscribe(
        data => {
          if (data['Success'] != null) {
            this.message = data['Success'];
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
              this.message = data['Error'];
              Swal.fire({
                position: 'top-end',
                icon: 'error',
                text: data['Error'],
                timer: 3500
              });
            } else {
              this.message = data['InternalErrors'];
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
  }

  registerWithFB() {
    this.socialAuthService.signIn(FacebookLoginProvider.PROVIDER_ID).then(
      response => {
        console.log(response);
        this.userFB = response;
        this.u.email = this.userFB.email;
        this.u.name = this.userFB.name;
        this.u.userName = this.userFB.email;
        this.u.image = this.userFB.photoUrl;

        this.authService.facebookregisterUser(this.u)
        .subscribe(
          data => {
            const res = Object.keys(data);
            const returnedValue = data[res[1]];
            const id = data[res[0]] as number;
            if (id === 2) {
              this.message = returnedValue;
              Swal.fire({
                position: 'top-end',
                title: 'Nicee...',
                text: returnedValue,
                icon: 'success',
                timer: 3500
              }).then(() => {
                this.router.navigate(['/']);
              });
            } else {
              if (id === 1) {
                this.message = returnedValue;
                Swal.fire({
                  position: 'top-end',
                  icon: 'error',
                  text: returnedValue,
                  timer: 3500
                });
              } else {
                this.message = returnedValue;
                Swal.fire({
                  position: 'top-end',
                  icon: 'error',
                  text: returnedValue,
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
    );  
  }

}
