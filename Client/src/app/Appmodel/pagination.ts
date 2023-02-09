export interface Pagination {
  currentPage: number;
  itemsPerPage: number;
  totalItems: number;
  totalPages: number;
}

export class PaginatedResult<T> {
  result?: T;
  pagination?: Pagination;
  static result: any;
  static pagination: any;
}
// export class PaginatedResult<T> {
//   result?: T;
//   pagination?: Pagination;
// }