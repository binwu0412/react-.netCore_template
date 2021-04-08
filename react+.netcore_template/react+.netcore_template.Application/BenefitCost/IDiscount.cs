using System;
using System.Collections.Generic;
using System.Text;

namespace react_.netcore_template.Application.BenefitCost
{
    public interface IDiscount<U> where U : class
    {
        bool CanBeApplied(U target);
        float GetDiscount(float benefitCost);
    }
}
