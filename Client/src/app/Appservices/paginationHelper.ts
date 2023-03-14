import { HttpClient, HttpParams } from "@angular/common/http";
import { map } from "rxjs";
import { PaginatedResult } from "../appModel/pagination";

export function getPaginatedResult<T>(url: string, params: HttpParams, http: HttpClient) {
 const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>;
 return http.get<T>(url, { observe: 'response', params }).pipe(
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

export function getPaginationHeaders(pageNumber: number, pageSize: number) {
 let params = new HttpParams();
 params = params.append('pageNumber', pageNumber);
 params = params.append('pageSize', pageSize);

 return params;
}