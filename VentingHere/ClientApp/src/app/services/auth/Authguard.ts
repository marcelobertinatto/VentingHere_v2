import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';

export class Authguard implements CanActivate {

    constructor(private router: Router) { }

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

        if (sessionStorage.getItem('token') !== null) {
            return true;
          } else {
            this.router.navigate(['/home'], { queryParams: { returnUrl: state.url }});
            return false;
          }
    }
}
