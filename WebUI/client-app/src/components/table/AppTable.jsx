import React from 'react';
import { Paper, Table, TableContainer, TableHead, TableRow, TableCell, TableBody, TableFooter, TablePagination } from '@material-ui/core';

const AppTable = ({ tableColumns, data, setRowProps, pageIndex, totalCount, totalPages, hasNextPage, 
  hasPreviousPage, handleChangePagination, pageSize, handleChangePage }) => {
  return (
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow>
            { tableColumns.map((column, index) => (
              <TableCell key={index} {...column.headerProps}>{column.title}</TableCell>
            ))}
          </TableRow>
        </TableHead>
        <TableBody>
          {data.map((item, indexI) => (
            <TableRow key={indexI} {...(Boolean(setRowProps) ? setRowProps(item, indexI) : null)}>
              { tableColumns.map((column, indexJ) => (
                <TableCell key={indexJ} {...column.cellProps}>{item[column.property]}</TableCell>
              ))}
            </TableRow>
          ))}
        </TableBody>
        <TableFooter>
          <TableRow>
            <TablePagination 
              colSpan={3}
              count={totalCount}
              rowsPerPage={pageSize}
              page={pageIndex}
              onChangePage={handleChangePage}
              SelectProps={{ onChange: handleChangePagination}}
            />
          </TableRow>
        </TableFooter>
      </Table>
    </TableContainer>
  );
}

export default AppTable;

