<<<<<<< HEAD
=======
import { empty } from '@angular-devkit/schematics';
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
<<<<<<< HEAD
import { environment } from 'src/environments/environment';
=======
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
import { User } from '../appModel/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
<<<<<<< HEAD
  getMember(username: string) {
    throw new Error('Method not implemented.');
  }
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();
=======
  baseUrl = 'https://localhost:7078/api/';
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
  constructor(private http: HttpClient) {

  }
  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
<<<<<<< HEAD
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
          // localStorage.setItem('user', JSON.stringify(user));
          // this.currentUserSource.next(user);
=======
      map((respone: User) => {
        const user = respone;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
        }
      })
    )
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
        if (user) {
<<<<<<< HEAD
          this.setCurrentUser(user);
          // localStorage.setItem('user', JSON.stringify(user));
          // this.currentUserSource.next(user);
=======
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
        }
        // return user;
      })
    )
  }
  setCurrentUser(user: User) {
<<<<<<< HEAD
    localStorage.setItem('user', JSON.stringify(user));
=======
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
<<<<<<< HEAD
    // this.currentUserSource.complete();
=======
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
    this.currentUserSource.next(null);
  }
}
