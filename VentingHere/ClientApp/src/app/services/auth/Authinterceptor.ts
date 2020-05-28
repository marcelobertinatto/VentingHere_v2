import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { tap } from 'rxjs/internal/operators/tap';


@Injectable({ providedIn: 'root'})
export class Authinterceptor implements HttpInterceptor {

    constructor(private router: Router) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (sessionStorage.getItem('token') !== null) {
            const token = 'Bearer ' + sessionStorage.getItem('token').replace(/^"(.*)"$/, '$1');
            const cloneReq = req.clone({
                headers: req.headers.set('Authorization', token)
            });
            return next.handle(cloneReq).pipe(
                tap(
                    succ => { },
                    err => {
                        if (err.status === 401) {
                            this.router.navigate(['/home']);
                        }
                    }
                )
            );
        } else {
            return next.handle(req.clone());
        }

    }
}
