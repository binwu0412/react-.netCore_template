using System;
using System.Collections.Generic;
using System.Text;

namespace react_.netcore_template.Application.Commons.Models
{
    public class BenefitCostPreviewDto
    {
        public float EmployeeBenefitCost { get; set; }

        public float DependendBenefitCost { get; set; }

        public float EmployeeBenefitCostDiscount { get; set; }

        public float DependendBenefitCostDiscount { get; set; }

        public float PaycheckAfterDeduction { get; set; }
    }
}
