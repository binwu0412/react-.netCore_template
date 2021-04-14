using System;
using NUnit.Framework;
using react_.netcore_template.Application.BenefitCost;
using react_.netcore_template.Domain.Entities;

namespace Test.Application.BenefitCost
{
    [TestFixture]
    public class StartWithATenPercentOffDiscountUnitTest
    {
        private readonly StartWithATenPercentOffDiscount _tenPercenDiscount;

        public StartWithATenPercentOffDiscountUnitTest()
        {
            _tenPercenDiscount = new StartWithATenPercentOffDiscount();
        }

        [Test]
        public void CanBeApplied_InputEmployeeWithNameA_ExpectTrue()
        {
            var employee = new Employee() { Name = "A Test" };

            var result = _tenPercenDiscount.CanBeApplied(employee);

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void CanBeApplied_InputEmployeeWithNameB_ExpectFalse()
        {
            var employee = new Employee() { Name = "B Test" };

            var result = _tenPercenDiscount.CanBeApplied(employee);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void CanBeApplied_InputDependendWithNameA_ExpectTrue()
        {
            var employee = new Dependend() { Name = "A Test" };

            var result = _tenPercenDiscount.CanBeApplied(employee);

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void CanBeApplied_InputDependendWithNameB_ExpectFalse()
        {
            var employee = new Dependend() { Name = "B Test" };

            var sut = _tenPercenDiscount.CanBeApplied(employee);

            Assert.That(sut, Is.EqualTo(false));
        }

        [Test]
        [TestCase(1f, 0.1f)]
        [TestCase(20.5f, 2.05f)]
        [TestCase(300.3f, 30.03f)]
        public void GetDiscount_testCases(float input, float caseResult)
        {
            var sut = _tenPercenDiscount.GetDiscount(input);

            Assert.That(sut, Is.EqualTo(caseResult).Within(.005));
        }

        [Test]
        public void GetDiscount_inputNegativeCost_expectArgExption()
        {
            Assert.Throws<ArgumentException>(
                () => { _tenPercenDiscount.GetDiscount(-1f); });
        }
    }
}
