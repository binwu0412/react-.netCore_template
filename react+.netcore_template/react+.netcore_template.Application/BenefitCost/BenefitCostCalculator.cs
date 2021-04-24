using react_.netcore_template.Application.Commons.Models;
using react_.netcore_template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace react_.netcore_template.Application.BenefitCost
{
    public class BenefitCostCalculator : IBenefitCostCalculator
    {
        public BenefitCostPreviewDto GetBenefitCostPreview(Employee employee, IList<Dependend> dependends)
        {

            var employeeCost = new EmployeeBenefitCost(employee);
            var employeeDiscountAmount = employeeCost.ApplyDiscount(new StartWithATenPercentOffDiscount());


            var dependendCostAmount = 0f;
            var dependentDiscountAmount = 0f;
            for (var i = 0; i < dependends.Count; i++)
            {
                var dependendBenefitCost = new DependendBenefitCost(dependends[i]);
                dependendCostAmount += dependendBenefitCost.Cost;
                dependentDiscountAmount += dependendBenefitCost.ApplyDiscount(new StartWithATenPercentOffDiscount());
            }

            //if (EmployeeWage.payPeriods == 0)
            //{
            //    throw new DivideByZeroException("Employee pay period can't be 0.");
            //}

            //if (EmployeeWage.payPeriods < 0)
            //{
            //    throw new Exception("Pay period can't be negative number.");
            //}

            // TO-DO: confirm whether paycheck can be negative
            return new BenefitCostPreviewDto
            {
                EmployeeBenefitCost = employeeCost.Cost,
                EmployeeBenefitCostDiscount = employeeDiscountAmount,
                DependendBenefitCost = dependendCostAmount,
                DependendBenefitCostDiscount = dependentDiscountAmount,
                PaycheckAfterDeduction = (employeeCost.Cost + dependendCostAmount - employeeDiscountAmount - dependentDiscountAmount) / EmployeeWage.payPeriods
            };
        }
    }
}
