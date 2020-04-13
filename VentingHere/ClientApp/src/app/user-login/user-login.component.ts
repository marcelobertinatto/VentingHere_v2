import { AuthService } from './../services/auth/AuthService';
import { ModalService } from './../services/Modal.service';
import { Component, OnInit } from '@angular/core';
import { NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CustomValidators } from '../helpers/customValidators';
import { Router } from '@angular/router';
import { User } from '../models/user';
import Swal from 'sweetalert2';

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

  constructor(private modal: ModalService, private formBuilder: FormBuilder, private authService: AuthService,
    private router: Router) {
  }

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
      if (data != null && data['Error'] === '') {
        sessionStorage.setItem('user-authenticated', JSON.stringify(data['user']));
        sessionStorage.setItem('token', JSON.stringify(data['token']));
        if (this.returnUrl == null) {
          // this.router.navigate(['/userspage']);
          this.router.navigate(['/home']);
        }
        this.router.navigate([this.returnUrl]);
      } else {
          Swal.fire({
            position: 'top-end',
            icon: 'error',
            title: data['Error'],
            showConfirmButton: false,
            timer: 3500,
            showCloseButton: true
          });
      }
    }, error => {
    });

  }

  close() {
    this.modal.close();
  }


}
