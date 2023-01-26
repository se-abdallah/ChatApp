import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../appModel/member';
import { PaginationResult } from '../appModel/pagination';

@Injectable({
  providedIn: 'root'
})
export class MembersService { 
  baseUrl = environment.apiUrl;
  members: Member[] = [];
  paginatedResult: PaginationResult<Member[]> = new PaginationResult<Member[]>;

  constructor(private http: HttpClient) { }

  getMembers(page?: number, itemsPerPage?: number) {
    let params = new HttpParams();
    if (page && itemsPerPage) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }
    return this.http.get<Member[]>(this.baseUrl + 'users', { observe: 'response', params }).pipe(
      map(response => {
        if(response.body){
          this.paginatedResult.result = response.body;
        }
        const Pagination = response.headers.get('pagination');
        if(Pagination){
          this.paginatedResult.pagination = JSON.parse(Pagination);
        }
        return this.paginatedResult;
      })


    )
  }
  // if (this.members.length > 0) return of(this.members);
  // return this.http.get<Member[]>(this.baseUrl + 'users').pipe(
  //   map(members => {
  //     this.members = members;
  //     return members;
  //   })


  getMember(username: string) {
    const member = this.members.find(x => x.userName === username);
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
