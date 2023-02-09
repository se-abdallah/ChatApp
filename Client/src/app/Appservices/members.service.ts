import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of, take } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../appModel/member';
import { PaginatedResult } from '../appModel/pagination';
import { User } from '../appModel/user';
import { UserParams } from '../appModel/userParams';
import { AccountService } from './account.service';


@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];
  memberCache = new Map();
  userParams!: UserParams;
  user!: User;
  // paginatedResult: PaginationResult<Member[]> = new PaginationResult<Member[]>;

  constructor(private http: HttpClient, private accountService: AccountService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) {
          this.userParams = new UserParams(user);
          this.user = user;
        }
      }
    })
  }
  resetUserParams(){
    if(this.user){
      this.userParams = new UserParams(this.user);
      return this.userParams;
    }
  }

  getUserParams(){
    return this.userParams;
  }
  setUserParams(params: UserParams){
    this.userParams = params;
  }
  getMembers(userParams: UserParams) {
    const response = this.memberCache.get(Object.values(userParams).join('-'));
    if (response) return of(response);
    let params = this.getPaginationHeaders(userParams.pageNumber, userParams.pageSize);
    params = params.append('MinAge', userParams.minAge);
    params = params.append('MaxAge', userParams.maxAge);
    params = params.append('gender', userParams.gender);
    params = params.append('orderBy', userParams.orderBy);
    return this.getPaginatedResult<Member[]>(this.baseUrl + 'users', params).pipe(
      map(response => {
        this.memberCache.set(Object.values(userParams).join('-'), response);
        return response;
      })
    )
  }

  private getPaginatedResult<T>(url: string, params: HttpParams) {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>;
    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        if (response.body) {
          paginatedResult.result = response.body;
        }
        const Pagination = response.headers.get('pagination');
        if (Pagination) {
          paginatedResult.pagination = JSON.parse(Pagination);
        }
        return paginatedResult;
      })
    );
  }

  private getPaginationHeaders(pageNumber: number, pageSize: number) {
    let params = new HttpParams();
    params = params.append('pageNumber', pageNumber);
    params = params.append('pageSize', pageSize);

    return params;
  }

  // if (this.members.length > 0) return of(this.members);
  // return this.http.get<Member[]>(this.baseUrl + 'users').pipe(
  //   map(members => {
  //     this.members = members;
  //     return members;
  //   })


  getMember(username: string) {
    const member = [...this.memberCache.values()]
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((member: Member) => member.userName === username);
    // const member = this.members.find(x => x.userName === username);
    if (member) return of(member);
    return this.http.get<Member>(this.baseUrl + 'users/' + username)
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = { ...this.members[index], ...member }
      })
    )
  }

  setMainPhoto(photoId: number) {
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photoId);
  }
  // !after adding jwt interceptor ,we do not need this
  // getHttpOptions(){
  //   const userString = localStorage.getItem('user');
  //   if(!userString) return;
  //   const user = JSON.parse(userString);
  //   return {
  //     headers : new HttpHeaders ({
  //       Authorization :'Bearer ' + user.token
  //     })
  //   }
  // }
}
