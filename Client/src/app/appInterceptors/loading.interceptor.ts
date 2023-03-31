import {
  HttpEvent, HttpHandler, HttpInterceptor, HttpRequest
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, delay, finalize, identity } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BusyService } from '../appServices/busy.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private busyService: BusyService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.busyService.busy();
    return next.handle(request).pipe(
      (environment.production ? identity : delay(1000)),
      finalize(() => {
        this.busyService.idle()
      })
    )
  }
}
