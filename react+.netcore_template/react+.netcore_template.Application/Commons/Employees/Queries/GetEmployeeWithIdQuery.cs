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
    public class GetEmployeeWithIdQuery : IRequest<EmployeeDto>, ICacheKey
    {
        public int Id { get; set; }

        public static string GetCacheKey(int id) => "getEmployeeWithIdQuery:" + id.ToString();

        public string GetCacheKey() => "getEmployeeWithIdQuery:" + Id.ToString();
    }

    public class GetEmployeeWithIdQueryHandler : IRequestHandler<GetEmployeeWithIdQuery, EmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICacheService _cacheService;

        public GetEmployeeWithIdQueryHandler(IEmployeeRepository employeeRepository, ICacheService cacheService)
        {
            _employeeRepository = employeeRepository;
            _cacheService = cacheService;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeWithIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _cacheService.GetCachedAsync<EmployeeDto>(request.GetCacheKey());
            if (result != null) return result;

            var employeeDto = await _employeeRepository.GetDtoAsync(request.Id);
            await _cacheService.CacheAsync(request.GetCacheKey(), employeeDto);
            return employeeDto;
        }
    }
}
