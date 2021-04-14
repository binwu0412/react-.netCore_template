using System;
using NUnit.Framework;
using react_.netcore_template.Application.BenefitCost;
using react_.netcore_template.Domain.Entities;

namespace Test.Application.BenefitCost
{
    [TestFixture]
    public class EmployeeBenefitCostUnitTest
    {
        private readonly EmployeeBenefitCost _employeeBenefitCost;
        private readonly Employee _employee;
        private readonly TestApllicableDiscount _testApllicableDiscount;
        private readonly TestNonApllicableDiscount _testNonApllicableDiscount;

        public EmployeeBenefitCostUnitTest()
        {
            _employee = new Employee()
            {
                Id = 0,
                Name = "chang",
                Title = "software engineer",
                Department = "IT"
            };
            _employeeBenefitCost = new EmployeeBenefitCost(_employee);
            _testApllicableDiscount = new TestApllicableDiscount();
            _testNonApllicableDiscount = new TestNonApllicableDiscount();
        }

        [Test]
        public void ApplyDiscount_InputApplicableDiscount_returnDiscountHalf()
        {
            var discountAmount = _employeeBenefitCost.ApplyDiscount(_testApllicableDiscount);

            Assert.That(discountAmount, Is.EqualTo(0.5f));
        }

        [Test]
        public void ApplyDiscount_InputApplicableDiscount_returnNoDiscount()
        {
            var discontAmount = _employeeBenefitCost.ApplyDiscount(_testNonApllicableDiscount);

            Assert.That(discontAmount, Is.EqualTo(0f));
        }

    }

    public class TestApllicableDiscount : IDiscount<Employee>
    {
        public bool CanBeApplied(Employee target)
        {
            return true;
        }

        public float GetDiscount(float benefitCost)
        {
            return 0.5f;
        }
    }

    public class TestNonApllicableDiscount : IDiscount<Employee>
    {
        public bool CanBeApplied(Employee target)
        {
            return false;
        }

        public float GetDiscount(float benefitCost)
        {
            return 0.5f;
        }
    }
}
