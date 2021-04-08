using react_.netcore_template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace react_.netcore_template.Application.BenefitCost
{
    public class DependendBenefitCost : IDiscountApplicable<Dependend>
    {
        public float Cost { get; protected set; } = 500f;

        private Dependend _dependend;

        public DependendBenefitCost(Dependend dependend)
        {
            _dependend = dependend;
        }

        public float ApplyDiscount(IDiscount<Dependend> discount)
        {
            if (discount.CanBeApplied(_dependend))
            {
                return discount.GetDiscount(Cost);
            }
            return 0f;
        }
    }
}
