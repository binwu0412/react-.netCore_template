using react_.netcore_template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace react_.netcore_template.Application.BenefitCost
{
    public class EmployeeBenefitCost : IDiscountApplicable<Employee>
    {
        public float Cost { get; protected set; } = 1000f;

        private Employee _employee;

        public EmployeeBenefitCost(Employee employee)
        {
            _employee = employee;
        }

        public float ApplyDiscount(IDiscount<Employee> discount)
        {
            if (discount.CanBeApplied(_employee))
            {
                return discount.GetDiscount(Cost);
            }

            return 0f;
        }
    }
}
