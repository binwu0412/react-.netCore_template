import { EmployeesWithPagination } from '../models/EmployeesWithPagination';
import httpClient from './HttpClient';
import * as ToastService from './ToastService';

export const getEmployees = () => {
  return httpClient.get("/employee/all")
    .then(resp => resp.data)
    .catch(error => {
      ToastService.showErrorToast("Error: couldn't fetch employees.");
      return [];
    });
}

export const getEmployeesWithPagination = (employeesWithPaginationQuery) => {
  let url = "/employee/all?";
  for (let prop in employeesWithPaginationQuery) {
    url = `${url}${prop}=${employeesWithPaginationQuery[prop]}&`
  }
  return httpClient.get(url)
    .then(resp => resp.data)
    .catch(error => {
      ToastService.showErrorToast("Error: couldn't fetch employee list");
      return new EmployeesWithPagination();
    });
}

export const searchEmployeeByName = (searchString, pageSize) => {
  return httpClient.get(`/employee/search?searchString=${searchString}&pageSize=${pageSize}`)
    .then(resp => resp.data)
    .catch(error => {
      ToastService.showErrorToast("Error: couldn't search employee by name.");
      return new EmployeesWithPagination();
    });
}
