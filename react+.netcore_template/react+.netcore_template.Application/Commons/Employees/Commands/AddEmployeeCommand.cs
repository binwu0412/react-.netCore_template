using MediatR;
using react_.netcore_template.Application.Commons.Cache.Commands;
using react_.netcore_template.Application.Commons.Employees.Queries;
using react_.netcore_template.Application.Commons.Interfaces;
using react_.netcore_template.Application.Commons.Models;
using react_.netcore_template.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace react_.netcore_template.Application.Commons.Employees.Commands
{
    public class AddEmployeeCommand : IRequest
    {
        public EmployeeDto EmployeeDto { get; set; }

        public List<IRequest> GetFollowupRequests()
            => new List<IRequest>() {
                new RemoveEmployeesWithPaginationCacheCommand(GetAllEmployeesWithPaginationQuery.GetCachePattern())};
    }

    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand>
    {

        private readonly IEmployeeRepository _employeeRepository;

        private readonly IMediator _mediator;

        public AddEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMediator mediator)
        {
            _employeeRepository = employeeRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee()
            {
                Name = request.EmployeeDto.Name,
                Title = request.EmployeeDto.Title,
                Department = request.EmployeeDto.Department
            };
            await _employeeRepository.AddAsync(employee);

            return Unit.Value;
        }

        public Task SendFollowups(AddEmployeeCommand request)
        {
            var followupTaskList = new List<Task>();
            for (var i = 0; i < request.GetFollowupRequests().Count; i++)
            {
                followupTaskList.Add(_mediator.Send(request.GetFollowupRequests()[i]));
            }
            return Task.WhenAll(followupTaskList);
        }
    }
}
