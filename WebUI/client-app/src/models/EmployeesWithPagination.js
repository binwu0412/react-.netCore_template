export class EmployeesWithPagination {
  items = [];
  pageIndex = 0;
  totalPages = 1;
  totalCount = 0;
  hasNextPage = false;
  hasPreviousePage = false;
}

export class EmployeesWithPaginationQuery {
  pageNumber;
  pageSize;

  constructor(pageNumber = 1, pageSize = 10) {
    this.pageNumber = pageNumber;
    this.pageSize = pageSize;
  }
}

export class SearchEmployeesWithPaginationQuery {
  pageNumber = 1;
  pageSize;
  searchString;

  constructor(searchString, pageSize = 10) {
    this.searchString = searchString;
    this.pageSize = pageSize
  }
}
