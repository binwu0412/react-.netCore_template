using react_.netcore_template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace react_.netcore_template.Application.BenefitCost
{
    public class StartWithATenPercentOffDiscount : IDiscount<Employee>, IDiscount<Dependend>
    {
        private float Rate = 0.1f;

        public bool CanBeApplied(Employee target)
        {
            return target.Name.StartsWith("A");
        }

        public bool CanBeApplied(Dependend target)
        {
            return target.Name.StartsWith("A");
        }

        public float GetDiscount(float benefitCost)
        {
            return benefitCost * Rate;
        }

    }
}
