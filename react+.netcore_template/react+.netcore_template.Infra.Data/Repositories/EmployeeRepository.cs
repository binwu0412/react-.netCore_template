using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using react_.netcore_template.Application.Commons.Interfaces;
using react_.netcore_template.Application.Commons.Models;
using react_.netcore_template.Domain.Entities;
using react_.netcore_template.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace react_.netcore_template.Infra.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        protected readonly EmployeeDbContext _context;
        protected readonly IMapper _mapper;

        public EmployeeRepository(EmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<Employee> GetAsync(int employeeId)
        {
            return _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
        }

        public async Task<EmployeeDto> GetDtoAsync(int employeeId)
        {
            var employeeDto = await _context.Employees
                .Where(e => e.Id == employeeId)
                .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (employeeDto == null)
            {
                throw new Exception("Employee not found");
            }

            return employeeDto;
        }

        public async Task<List<EmployeeDto>> GetAllAsync()
        {
            var employees = await _context.Employees.ToListAsync();
            return _mapper.Map<List<Employee>, List<EmployeeDto>>(employees);
        }

        public async Task<Employee> GetEmployeeWithDependendAsync(int employeeId)
        {
            var employee = await _context.Employees.Include(e => e.Dependends).FirstOrDefaultAsync(e => e.Id == employeeId);
            if (employee == null)
            {
                throw new Exception("Employee not found");
            }
            return employee;
        }

        public Task<PaginatedList<EmployeeDto>> GetPaginedListAsync(int pageNumber, int pageSize)
        {
            var employeesSource = _context.Employees
                    .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider);
            return PaginatedList<EmployeeDto>.CreateAsync(employeesSource, pageNumber - 1, pageSize);
        }

        public Task<PaginatedList<EmployeeDto>> GetPaginatedSearchListAsync(int pageSize, string searchString)
        {
            var employeesSource = _context.Employees
                    .Where(e => EF.Functions.FreeText(e.Name, searchString))
                    .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider);
            return PaginatedList<EmployeeDto>.CreateAsync(employeesSource, 0, pageSize);
        }

        public Task AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            return _context.SaveChangesAsync();
        }

        public Task UpdateAsync(Employee employee)
        {
            var existingEmployee = _context.Employees.FirstOrDefaultAsync(e => e.Id == employee.Id);
            if (existingEmployee == null)
            {
                throw new Exception("Employee not found");
            }

            _context.Employees.Update(employee);
            return _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int employeeId)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == employeeId);
            if (employee == null)
            {
                throw new Exception("Employee not found");
            }

            _context.Employees.Remove(employee);
            return _context.SaveChangesAsync();
        }
    }
}
