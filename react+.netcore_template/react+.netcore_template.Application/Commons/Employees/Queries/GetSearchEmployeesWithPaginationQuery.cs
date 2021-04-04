using MediatR;
using react_.netcore_template.Application.Commons.Interfaces;
using react_.netcore_template.Application.Commons.Models;
using System.Threading;
using System.Threading.Tasks;

namespace react_.netcore_template.Application.Commons.Employees.Queries
{
    public class GetSearchEmployeesWithPaginationQuery : IRequest<PaginatedList<EmployeeDto>>
    {
        public string SearchString { get; set; }
        public int PageSize { get; set; }
    }

    public class GetSearchEmployeesWithPaginationQueryHandler : IRequestHandler<GetSearchEmployeesWithPaginationQuery, PaginatedList<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetSearchEmployeesWithPaginationQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<PaginatedList<EmployeeDto>> Handle(GetSearchEmployeesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var employeeDtoList = await _employeeRepository.GetPaginatedSearchListAsync(request.PageSize, request.SearchString);
            return employeeDtoList;
        }
    }
}
