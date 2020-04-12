import { ModalService } from './../services/Modal.service';
import { Component, OnInit } from '@angular/core';
import { NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CustomValidators } from '../helpers/customValidators';

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

  constructor(private modal: ModalService, private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.validation();
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

  }

  close() {
    this.modal.close();
  }


}
