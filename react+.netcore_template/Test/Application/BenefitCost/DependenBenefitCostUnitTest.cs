using System;
using NUnit.Framework;
using react_.netcore_template.Application.BenefitCost;
using react_.netcore_template.Domain.Entities;

namespace Test.Application.BenefitCost
{
    [TestFixture]
    public class DependenBenefitCostUnitTest
    {
        private readonly DependendBenefitCost _dependenBenefitCost;
        private readonly Dependend _dependend;
        private readonly TestDiscount _testApllicableDiscount;
        private readonly TestNoDiscount _testNonApllicableDiscount;

        public DependenBenefitCostUnitTest()
        {
            _dependend = new Dependend()
            {
                Id = 0,
                Name = "chang",
                EmployeeId = 0
            };

            _dependenBenefitCost = new DependendBenefitCost(_dependend);
            _testApllicableDiscount = new TestDiscount();
            _testNonApllicableDiscount = new TestNoDiscount();
        }

        [Test]
        public void ApplyDiscount_InputApplicableDiscount_returnDiscountHalf()
        {
            var discountAmount = _dependenBenefitCost.ApplyDiscount(_testApllicableDiscount);

            Assert.That(discountAmount, Is.EqualTo(0.5f));
        }

        [Test]
        public void AppluDiscount_InputApplicableDiscount_returnNoDiscount()
        {
            var discountAmount = _dependenBenefitCost.ApplyDiscount(_testNonApllicableDiscount);

            Assert.That(discountAmount, Is.EqualTo(0f));
        }
    }
    public class TestDiscount : IDiscount<Dependend>
    {
        public bool CanBeApplied(Dependend target)
        {
            return true;
        }

        public float GetDiscount(float benefitCost)
        {
            return 0.5f;
        }
    }

    public class TestNoDiscount : IDiscount<Dependend>
    {
        public bool CanBeApplied(Dependend target)
        {
            return false;
        }

        public float GetDiscount(float benefitCost)
        {
            return 0.5f;
        }
    }
}
