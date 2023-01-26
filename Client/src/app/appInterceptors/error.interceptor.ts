import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) {

  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error:HttpErrorResponse) => {
        if (error) {
          switch (error.status) {
            case 400:
              if (error.error.errors) {
                // const modalStateErrors: any[] = [];
                const modelStateErrors : any = [];
                for (const key in error.error.errors) {
                  if (error.error.errors[key]) {
                    modelStateErrors.push(error.error.errors[key])
                  }
                }

                throw modelStateErrors.flat();
              }
               else {
                this.toastr.error(error.error, error.status.toString())
               }
              //   this.toastr.error(error.statusText, error.status.toString());
              // }
              //  if (typeof (error.error) === 'object') {
              //   this.toastr.error(error.statusText, error.status.toString());
              // } else {
              //   this.toastr.error(error.error, error.status.toString());
              // }
              break;
            case 401:
              this.toastr.error('Unauthorised', error.status.toString());
              break;
            case 404:
              this.router.navigateByUrl('/not-found');
              break;
            case 500:
              const navigationExtras: NavigationExtras = { state: { error: error.error } };
              this.router.navigateByUrl('/server-error', navigationExtras);
              break;
            default:
              this.toastr.error('Something unexpected went wrong');
              console.log(error);
              break;
          }
        }
        //return throwError(error);
        // return throwError(() => error);
        // return throwError(() => new Error(error));
        throw error;
      })
    )
  }
}
