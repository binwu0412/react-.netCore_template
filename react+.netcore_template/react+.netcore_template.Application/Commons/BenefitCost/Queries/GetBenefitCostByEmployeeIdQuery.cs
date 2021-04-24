using MediatR;
using react_.netcore_template.Application.BenefitCost;
using react_.netcore_template.Application.Commons.Interfaces;
using react_.netcore_template.Application.Commons.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace react_.netcore_template.Application.Commons.BenefitCost.Queries
{
    public class GetBenefitCostByEmployeeIdQuery : IRequest<BenefitCostPreviewDto>, ICacheKey
    {
        public int EmployeeId { get; set; }

        public static string GetCacheKey(int employeeId) => "getBenefitCostByEmployeeIdQuery:" + employeeId;

        public string GetCacheKey() => "getBenefitCostByEmployeeIdQuery:" + EmployeeId;
    }

    public class GetBenefitCostByEmployeeIdQueryHandler : IRequestHandler<GetBenefitCostByEmployeeIdQuery, BenefitCostPreviewDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICacheService _cacheService;
        private readonly IBenefitCostCalculator _benefitCostCalculator;

        public GetBenefitCostByEmployeeIdQueryHandler(IEmployeeRepository employeeRepository, ICacheService cacheService, IBenefitCostCalculator benefitCostCalculator)
        {
            _employeeRepository = employeeRepository;
            _cacheService = cacheService;
            _benefitCostCalculator = benefitCostCalculator;
        }

        public async Task<BenefitCostPreviewDto> Handle(GetBenefitCostByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _cacheService.GetCachedAsync<BenefitCostPreviewDto>(request.GetCacheKey());
            if (result != null) return result;

            var employeeWithDependend = await _employeeRepository.GetEmployeeWithDependendAsync(request.EmployeeId);
            var benefitCost = _benefitCostCalculator.GetBenefitCostPreview(employeeWithDependend, employeeWithDependend.Dependends);

            await _cacheService.CacheAsync(request.GetCacheKey(), benefitCost);

            return benefitCost;
        }
    }
}
