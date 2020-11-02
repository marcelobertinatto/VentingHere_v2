import { AuthService } from './../services/auth/AuthService';
import { ModalService } from './../services/Modal.service';
import { Component, OnInit } from '@angular/core';
import { NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CustomValidators } from '../helpers/customValidators';
import { Router } from '@angular/router';
import { User } from '../models/user';
import Swal from 'sweetalert2';
import { SocialAuthService, FacebookLoginProvider, SocialUser } from 'angularx-social-login';

const ngbModalOptions: NgbModalOptions = {
  backdrop: 'static',
  keyboard : false,
  size: 'lg',
  scrollable: true,
  windowClass: 'my-class'
};

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {
  loginForm: FormGroup;
  public returnUrl: string;
  public u: User;
  userFB: SocialUser;
  loggedIn: boolean;
  faceImage: boolean;

  constructor(private modal: ModalService, private formBuilder: FormBuilder, private authService: AuthService,
    private router: Router
     , private socialAuthService: SocialAuthService) {}
  

  ngOnInit(): void {
    this.validation();
    this.u = new User();    
  }

  validation() {
    this.loginForm = this.formBuilder.group({
      userEmail: [null, Validators.compose([Validators.required, Validators.email])],
      userPassword: [null,
        Validators.compose([
          // 1. Password Field is Required
          Validators.required,
          // 2. check whether the entered password has a number
          CustomValidators.patternValidator(/\d/, { hasNumber: true }),
          // 3. At least one letter
          CustomValidators.patternValidator(/[a-zA-Z]/, { hasLetter: true }),
          Validators.minLength(6)])
      ],
      rememberMe: [false, Validators.compose([Validators.requiredTrue])]
    });
  }

  submit() {
    const formValues = this.loginForm.value;
    this.u.email = formValues.userEmail;
    this.u.password = formValues.userPassword;

    return this.authService.login(this.u)
    .subscribe(data => {
      const res = Object.keys(data);
      const token = data[res[0]];
      const roles = JSON.parse(window.atob(token.split('.')[1]))['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']; 
      const returnedValue = data[res[1]] as User;
      returnedValue.facebookImage = false;
      returnedValue.roles = roles;
      const id = data[res[2]] as number;
      if (id === 2) {
        sessionStorage.setItem('user-authenticated', JSON.stringify(returnedValue));
        sessionStorage.setItem('token', JSON.stringify(token));
        this.close();
        if (this.returnUrl == null) {
          this.router.navigate(['/userspage']);
        }
        this.router.navigate([this.returnUrl]);
      } else {
          Swal.fire({
            position: 'top-end',
            icon: 'error',
            title: returnedValue,
            showConfirmButton: false,
            timer: 3500,
            showCloseButton: true
          });
      }
    });

  }

  close() {
    this.modal.close();
  }

  signInWithFB() {
    this.socialAuthService.signIn(FacebookLoginProvider.PROVIDER_ID).then(
      response => {
        console.log(response);
        this.userFB = response;
        this.u.email = this.userFB.email;
        this.u.name = this.userFB.name;
        this.u.image = this.userFB.photoUrl;

        return this.authService.facebooklogin(this.u)
          .subscribe(data => {
            const res = Object.keys(data);
            const token = data[res[0]];
            const returnedValue = data[res[1]] as User;
            returnedValue.facebookImage = true;
            const id = data[res[2]] as number;
          if (id === 2) {
            sessionStorage.setItem('user-authenticated', JSON.stringify(returnedValue));
            sessionStorage.setItem('token', JSON.stringify(token));
            this.close();
            if (this.returnUrl == null) {
              this.router.navigate(['/userspage']);
            }
            this.router.navigate([this.returnUrl]);
          } else {
              Swal.fire({
                position: 'top-end',
                icon: 'error',
                title: returnedValue,
                showConfirmButton: false,
                timer: 3500,
                showCloseButton: true
              });
          }
        });
      }
    );  
  }  
 
  signOut() {
    this.socialAuthService.signOut().then(
      response => {
        this.userFB = null;
      }      
    );
  }

}
