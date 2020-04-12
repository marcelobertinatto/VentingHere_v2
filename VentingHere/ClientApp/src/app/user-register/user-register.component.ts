import { Component, OnInit } from '@angular/core';
import { ModalService } from '../services/Modal.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CustomValidators } from '../helpers/customValidators';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit {

  registerform: FormGroup;

  constructor(private modal: ModalService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.validation();
  }

  close() {
    this.modal.close();
  }

  validation() {
    this.registerform = this.formBuilder.group({
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
      firstName: [null, Validators.compose([Validators.required])],
      secondName: [null, Validators.compose([Validators.required])]
    });
  }

}
