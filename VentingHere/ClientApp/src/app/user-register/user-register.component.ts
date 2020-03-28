import { Component, OnInit } from '@angular/core';
import { ModalService } from '../services/Modal.service';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit {

  constructor(private modal: ModalService) { }

  ngOnInit() {
  }

  close() {
    this.modal.close();
  }

}
