using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using react_.netcore_template.Application.BenefitCost;
using react_.netcore_template.Application.Commons.BenefitCost.Queries;
using react_.netcore_template.Application.Commons.Interfaces;
using react_.netcore_template.Application.Commons.Models;
using react_.netcore_template.Domain.Entities;

namespace Test.Application.Commons.BenefitCost.Queries
{
    [TestFixture]
    public class GetBenefitCostByEmployeeIdQueryUnitTest
    {
        private Mock<IEmployeeRepository> _mockEmployeeRepository;
        private Mock<ICacheService> _mockCacheService;
        private Mock<IBenefitCostCalculator> _mockBenefitCostCalculator;

        [OneTimeSetUp]
        public void Init()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockCacheService = new Mock<ICacheService>();
            _mockBenefitCostCalculator = new Mock<IBenefitCostCalculator>();
        }

        [SetUp]
        public void Reset()
        {
            _mockEmployeeRepository.Reset();
            _mockCacheService.Reset();
            _mockBenefitCostCalculator.Reset();
        }

        [Test]
        public async Task Handle_HandleCache_ExpectGetBenefitCostByEmployeeIdQuery()
        {
            var testEmployee = new Employee() { Id = 1 };
            _mockEmployeeRepository.Setup(m => m.GetEmployeeWithDependendAsync(1)).ReturnsAsync(testEmployee);
            var testResult = new BenefitCostPreviewDto();
            var testQuery = new GetBenefitCostByEmployeeIdQuery() { EmployeeId = testEmployee.Id };
            _mockCacheService.Setup(m => m.GetCachedAsync<BenefitCostPreviewDto>(testQuery.GetCacheKey()))
                .ReturnsAsync(testResult);
            _mockBenefitCostCalculator.Setup(m => m.GetBenefitCostPreview(testEmployee, testEmployee.Dependends))
                .Returns(testResult);
            var _handler = new GetBenefitCostByEmployeeIdQueryHandler(
                _mockEmployeeRepository.Object, _mockCacheService.Object, _mockBenefitCostCalculator.Object);

            var sut = await _handler.Handle(testQuery, new CancellationToken());

            _mockEmployeeRepository.Verify(m => m.GetEmployeeWithDependendAsync(It.Is<int>(x => x == 1)), Times.Never());
            Assert.That(sut, Is.EqualTo(testResult));
        }

        [Test]
        public async Task Handle_HandleNoCache_ExpectGetBenefitCostByEmployeeIdQuery()
        {
            var testEmployee = new Employee() { Id = 2 };
            _mockEmployeeRepository.Setup(m => m.GetEmployeeWithDependendAsync(2)).ReturnsAsync(testEmployee);
            var testQuery = new GetBenefitCostByEmployeeIdQuery() { EmployeeId = testEmployee.Id };
            _mockCacheService.Setup(m => m.GetCachedAsync<BenefitCostPreviewDto>(testQuery.GetCacheKey()))
                .ReturnsAsync((BenefitCostPreviewDto)null);
            var testResult = new BenefitCostPreviewDto();
            _mockBenefitCostCalculator.Setup(m => m.GetBenefitCostPreview(testEmployee, testEmployee.Dependends))
                .Returns(testResult);
            var _handler = new GetBenefitCostByEmployeeIdQueryHandler(
                _mockEmployeeRepository.Object, _mockCacheService.Object, _mockBenefitCostCalculator.Object);

            var sut = await _handler.Handle(testQuery, new CancellationToken());

            _mockEmployeeRepository.Verify(m => m.GetEmployeeWithDependendAsync(It.Is<int>(x => x == 2)));
            Assert.That(sut, Is.EqualTo(testResult));
        }
    }
}
