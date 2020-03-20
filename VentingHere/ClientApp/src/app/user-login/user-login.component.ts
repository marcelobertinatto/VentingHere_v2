import { ModalService } from './../services/Modal.service';
import { Component } from '@angular/core';
import { NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

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
export class UserLoginComponent {
  constructor(private modal: ModalService) {
  }

  close() {
    this.modal.close();
  }
}
