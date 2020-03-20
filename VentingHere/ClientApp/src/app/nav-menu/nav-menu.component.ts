import { Component, Input, TemplateRef, OnInit, ViewChild } from '@angular/core';
import { NgbModal, ModalDismissReasons, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { UserLoginComponent } from '../user-login/user-login.component';
import { ModalService } from '../services/Modal.service';

const ngbModalOptions: NgbModalOptions = {
  backdrop: 'static',
  keyboard : false,
  size: 'lg',
  scrollable: true,
  windowClass: 'my-class'
};

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  @ViewChild(UserLoginComponent) userLoginComponent: UserLoginComponent;

  isExpanded = false;
  title = 'appBootstrap';
  closeResult: string;

  constructor(private modalService: ModalService) { }
  ngOnInit(): void {
  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  open() {
    this.modalService.open(UserLoginComponent, ngbModalOptions);
  }  

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return  `with: ${reason}`;
    }
  }
}
