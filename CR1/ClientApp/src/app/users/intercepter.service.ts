import { Injectable,Injector } from '@angular/core';
import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import {  UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class IntercepterService implements HttpInterceptor {

    constructor(private injector: Injector) { }
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let userService = this.injector.get(UserService);

        let tokenizedreq = this.addtokemheader(req, userService.gettoken());
        return next.handle(tokenizedreq).pipe(
            catchError(this.handleerror)
        );
       
    }

    handleerror(error: HttpErrorResponse) {
        console.log(error);
        return throwError(error);
    }

    addtokemheader(req: HttpRequest<any>, token: any) {
        let userService = this.injector.get(UserService);
        return req.clone({
            setHeaders: {
                Authorization: `Bearer ` + token
            }

        })
    }

  
}
