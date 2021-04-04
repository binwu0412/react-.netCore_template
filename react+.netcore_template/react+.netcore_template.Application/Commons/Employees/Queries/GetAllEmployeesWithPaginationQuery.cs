using MediatR;
using react_.netcore_template.Application.Commons.Interfaces;
using react_.netcore_template.Application.Commons.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace react_.netcore_template.Application.Commons.Employees.Queries
{
    public class GetAllEmployeesWithPaginationQuery : IRequest<PaginatedList<EmployeeDto>>, ICacheKey
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public static string GetCachePattern() => "getAllEmployeesWithPaginationQuery:*";

        public string GetCacheKey() => "getAllEmployeesWithPaginationQuery:" + "pageNumber:" + PageNumber + "pageSize" + PageSize;
    }

    public class GetAllEmployeesWithPaginationQueryHandler : IRequestHandler<GetAllEmployeesWithPaginationQuery, PaginatedList<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICacheService _cacheService;

        public GetAllEmployeesWithPaginationQueryHandler(IEmployeeRepository employeeRepository, ICacheService cacheService)
        {
            _employeeRepository = employeeRepository;
            _cacheService = cacheService;
        }

        public async Task<PaginatedList<EmployeeDto>> Handle(GetAllEmployeesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            //var result = await _cacheService.GetCachedAsync<PaginatedList<EmployeeDto>>(request.GetCacheKey());
            //if (result != null) return result; 

            var employeeDtoList = await _employeeRepository.GetPaginedListAsync(request.PageNumber, request.PageSize);
            await _cacheService.CacheAsync(request.GetCacheKey(), employeeDtoList);
            return employeeDtoList;
        }
    }
}
