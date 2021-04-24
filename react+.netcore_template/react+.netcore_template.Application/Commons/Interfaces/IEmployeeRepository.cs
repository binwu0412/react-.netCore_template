using react_.netcore_template.Application.Commons.Models;
using react_.netcore_template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace react_.netcore_template.Application.Commons.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetAsync(int employeeId);

        Task<EmployeeDto> GetDtoAsync(int employeeId);

        Task<List<EmployeeDto>> GetAllAsync();

        Task<Employee> GetEmployeeWithDependendAsync(int employeeId);

        Task<PaginatedList<EmployeeDto>> GetPaginatedSearchListAsync(int pageSize, string searchString);

        Task<PaginatedList<EmployeeDto>> GetPaginedListAsync(int pageNumber, int pageSize);

        Task AddAsync(Employee employee);

        Task UpdateAsync(Employee employee);

        Task DeleteAsync(int employeeId);
    }
}
