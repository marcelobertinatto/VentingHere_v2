import { AuthService } from './../services/auth/AuthService';
import { UserRegisterComponent } from './../user-register/user-register.component';
import { Component, OnInit, ViewChild, ElementRef, AfterViewInit, HostListener } from '@angular/core';
import { NgbModal, ModalDismissReasons, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { UserLoginComponent } from '../user-login/user-login.component';
import { ModalService } from '../services/Modal.service';
import { Router } from '@angular/router';

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
export class NavMenuComponent implements OnInit, AfterViewInit {
  @ViewChild(UserLoginComponent) userLoginComponent: UserLoginComponent;
  @ViewChild(UserRegisterComponent) userRegisterComponent: UserRegisterComponent;
  @ViewChild('stickyMenu') menuElement: ElementRef;

  isExpanded = false;
  title = 'appBootstrap';
  closeResult: string;
  sticky = false;
  menuPosition: any;

  constructor(private modalService: ModalService, private authService: AuthService, private router: Router) { }
  ngOnInit(): void {
  }

  ngAfterViewInit() {
    this.menuPosition = this.menuElement.nativeElement.offsetTop;
}

@HostListener('window:scroll', ['$event'])
    handleScroll() {
        const windowScroll = window.pageYOffset;
        if (windowScroll >= this.menuPosition) {
            this.sticky = true;
        } else {
            this.sticky = false;
        }
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

  register() {
    this.modalService.register(UserRegisterComponent, ngbModalOptions);
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

  public user_Authenticated() {
    return this.authService.user_Authenticated();
  }

  get user() {
    return this.authService.user;
  }

  public logout() {
    this.authService.session_cleaner();
    this.router.navigate(['/']);
  }
}
