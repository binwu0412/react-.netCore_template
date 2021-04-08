using MediatR;
using react_.netcore_template.Application.Commons.Cache.Commands;
using react_.netcore_template.Application.Commons.Employees.Queries;
using react_.netcore_template.Application.Commons.Interfaces;
using react_.netcore_template.Application.Commons.Models;
using react_.netcore_template.Domain.Bus;
using react_.netcore_template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace react_.netcore_template.Application.Commons.Employees.Commands
{
    public class UpdateEmployeeCommand : IRequest
    {
        public EmployeeDto EmployeeDto { get; set; }

        public List<IRequest> GetFollowupRequests()
            => new List<IRequest>() {
                new RemoveEmployeesWithPaginationCacheCommand(GetAllEmployeesWithPaginationQuery.GetCachePattern()),
                new RemoveEmployeeWithIdCacheCommand(GetEmployeeWithIdQuery.GetCacheKey(EmployeeDto.Id))};
    }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IMediator _mediator;

        private readonly IEmployeeRepository _employeeRepository;

        private readonly IDependendRepository _dependendRepository;

        private readonly IEventBus _eventBus;

        public UpdateEmployeeCommandHandler(IMediator mediator, IEmployeeRepository employeeRepository, IDependendRepository dependendRepository, IEventBus eventBus)
        {
            _mediator = mediator;
            _employeeRepository = employeeRepository;
            _dependendRepository = dependendRepository;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var taskList = new List<Task>();
            taskList.Add(UpdateEmployeeAsync(request.EmployeeDto));
            taskList.Add(UpdateDependendsAsync(request.EmployeeDto));

            await Task.WhenAll(taskList);

            // TO-DO: using event broker or message broker to send out message
            // to other microservices or serverless functions to handle following events
            await SendFollowupRequestAsync(request);

            return Unit.Value;
        }

        private Task SendFollowupRequestAsync(UpdateEmployeeCommand request)
        {
            var tasks = new List<Task>();
            for (var i = 0; i < request.GetFollowupRequests().Count; i++)
            {
                tasks.Add(_mediator.Send(request.GetFollowupRequests()[i]));
            }
            return Task.WhenAll(tasks);
        }

        private Task UpdateDependendsAsync(EmployeeDto employeeDto)
        {
            var dependendList = new List<Dependend>();
            for (var i = 0; i < employeeDto.Dependends.Count; i++)
            {
                dependendList.Add(new Dependend() { Name = employeeDto.Dependends[i].Name, Id = employeeDto.Dependends[i].Id });
            }
            return _dependendRepository.UpdateAndDeleteRangeAsync(dependendList, employeeDto.Id);
        }

        private Task UpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Title = employeeDto.Title
            };
            return _employeeRepository.UpdateAsync(employee);
        }
    }
}
