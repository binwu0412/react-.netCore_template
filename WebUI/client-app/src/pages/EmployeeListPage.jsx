import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import { Grid, Button } from "@material-ui/core";

import SearchBar from "../components/form/SearchBar";
import AppTable from "../components/table/AppTable";

import * as EmployeeService from "../services/EmployeeService";
import * as ToastService from "../services/ToastService";
import { EmployeesWithPaginationQuery } from "../models/EmployeesWithPagination";

const EmployeeListPage = () => {
  const tableColumns = [
    {title: "Employee", property: "name", cellProps: {component: "th", scope: "row"}},
    {title: "Department", property: "department", headerProps: {align: "center"}, cellProps: {align: "center"}},
    {title: "Title", property: "title", headerProps: {align: "right"}, cellProps: {align: "right"}}
  ];

  const [employees, setEmployees] = useState([]);
  const [pageIndex, setPageIndex] = useState(0);
  const [totalPages, setTotalPages] = useState(1);
  const [totalCount, setTotalCount] = useState(0);
  const [pageSize, setPageSize] = useState(10);
  const [pageNumber, setPageNumber] = useState(1);
  const handleLoadEmployees = async () => {
    const employeesWithPagination = await EmployeeService.getEmployeesWithPagination(new EmployeesWithPaginationQuery(pageNumber, pageSize));
    setEmployees(employeesWithPagination.items);
    setPageIndex(employeesWithPagination.pageIndex);
    setTotalPages(employeesWithPagination.totalPages);
    setTotalCount(employeesWithPagination.totalCount);
  }

  const handleChangePagination = (event) => {
    setPageSize(event.target.value);
    setPageNumber(1);
    handleLoadEmployees();
  }

  const handleChangePage = (event, page) => {
    setPageIndex(page + 1);
    setPageNumber(page + 1);
    handleLoadEmployees();
  }

  const handleNextButtonClick = () => {
    setPageNumber(pageNumber + 1);
    setPageIndex(pageIndex + 1);
    handleLoadEmployees();
  }

  const handleBackButtonClick = () => {
    setPageNumber(pageNumber - 1);
    setPageIndex(pageIndex - 1);
    handleLoadEmployees();
  }
  
  const [searchString, setSearchString] = useState("");
  const handleSearch = async (event) => {
    const employeesWithPagination = await EmployeeService.searchEmployeeByName(searchString, pageSize);
    setEmployees(employeesWithPagination.items);
    setPageIndex(employeesWithPagination.pageIndex);
    setTotalPages(employeesWithPagination.totalPages);
    setTotalCount(employeesWithPagination.totalCount);
  }

  const handleUpdateSearchString = (event) => setSearchString(event.target.value);

  const history = useHistory();
  const handleClickRow = (employee) => () => history.push({ 
    pathname: `/employeecosts/${employee.id}`, state: { employee: employee } 
  });

  useEffect(() => {
    handleLoadEmployees();
  }, []);

  return (
    <div className="page">
      <Grid container justify="center" className="pt-10per height-100per">
        <Grid item xs={6}>
          <div className="vertical-center horizontal-space-between mb-1">
            <div>
              <Button color="primary" variant="contained" onClick={() => ToastService.showErrorToast("Not Authorized.")}>
                New Employee
              </Button>
            </div>
            <SearchBar onClick={handleSearch} onChange={handleUpdateSearchString}/>
          </div>
          <AppTable 
            data={employees} 
            tableColumns={tableColumns}
            totalCount={totalCount}
            totalPages={totalPages}
            pageIndex={pageIndex}
            pageSize={pageSize}
            handleChangePage={handleChangePage}
            handleChangePagination={handleChangePagination}
            setRowProps={(item, index) => ({ onClick: handleClickRow(item) })}
          />
        </Grid>
      </Grid>
    </div>
  );
}

export default EmployeeListPage;