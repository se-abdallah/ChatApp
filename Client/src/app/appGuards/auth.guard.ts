import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, Observable } from 'rxjs';
import { AccountService } from '../appServices/account.service';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private accountService: AccountService, private toastr: ToastrService) {

  }
  canActivate(): Observable<boolean> {

    return this.accountService.currentUser$.pipe(
      map(user => {
        if (user) return true;
<<<<<<< HEAD
        else{
        this.toastr.error("You Shall not Pass");
        return false;
        }
=======
        this.toastr.error("You Shall not Pass");
        return false;
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
      })
    );
  }

}
