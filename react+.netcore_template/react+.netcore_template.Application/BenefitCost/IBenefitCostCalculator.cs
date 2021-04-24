using System;
using System.Collections.Generic;
using react_.netcore_template.Application.Commons.Models;
using react_.netcore_template.Domain.Entities;

namespace react_.netcore_template.Application.BenefitCost
{
    public interface IBenefitCostCalculator
    {
        BenefitCostPreviewDto GetBenefitCostPreview(Employee employee, IList<Dependend> dependends);
    }
}
