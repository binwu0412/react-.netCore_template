using MediatR;
using Microsoft.AspNetCore.Mvc;
using react_.netcore_template.Application.Commons.BenefitCost.Queries;
using react_.netcore_template.Application.Commons.Models;
using System.Threading.Tasks;

namespace react_.netcore_template.Web.BenefitCalculator.Controllers
{
    public class BenefitCostController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public BenefitCostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<ActionResult<BenefitCostPreviewDto>> GetBenefitCostByEmployeeId([FromQuery] GetBenefitCostByEmployeeIdQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
