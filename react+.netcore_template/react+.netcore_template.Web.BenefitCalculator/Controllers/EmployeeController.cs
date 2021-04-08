using MediatR;
using Microsoft.AspNetCore.Mvc;
using react_.netcore_template.Application.Commons.Employees.Commands;
using react_.netcore_template.Application.Commons.Employees.Queries;
using react_.netcore_template.Application.Commons.Models;
using System.Threading.Tasks;

namespace react_.netcore_template.Web.BenefitCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<ActionResult<PaginatedList<EmployeeDto>>> GetAllEmployeesWithPagination([FromQuery] GetAllEmployeesWithPaginationQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeWithId([FromQuery] GetEmployeeWithIdQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("search")]
        public async Task<ActionResult<PaginatedList<EmployeeDto>>> GetEmployeesByName([FromQuery] GetSearchEmployeesWithPaginationQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("")]
        public async Task<ActionResult<Unit>> AddEmployee([FromBody] AddEmployeeCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
