import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeBannerComponent } from './home-banner/home-banner.component';
import { HomeBodyComponent } from './home-body/home-body.component';
import { FooterComponent } from './footer/footer.component';
import { UserLoginComponent } from './user-login/user-login.component';
import {NgbModule, NgbAlertModule} from '@ng-bootstrap/ng-bootstrap';
import { UserRegisterComponent } from './user-register/user-register.component';
import { Authinterceptor } from './services/auth/Authinterceptor';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { UsersPageComponent } from './users-page/users-page.component';

@NgModule({
   declarations: [
      AppComponent,
      NavMenuComponent,
      HomeBannerComponent,
      HomeBodyComponent,
      FooterComponent,
      UserLoginComponent,
      UserRegisterComponent,
      UsersPageComponent
   ],
   imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeBodyComponent, pathMatch: 'full' },
      { path: 'login', component: UserLoginComponent },
      { path: 'userspage', component: UsersPageComponent }
    ]),
    NgbModule,
    NgbAlertModule,
    ReactiveFormsModule,
    SweetAlert2Module.forRoot()
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: Authinterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
