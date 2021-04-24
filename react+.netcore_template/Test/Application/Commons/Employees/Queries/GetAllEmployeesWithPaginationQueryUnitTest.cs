using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using react_.netcore_template.Application.Commons.Employees.Queries;
using react_.netcore_template.Application.Commons.Interfaces;
using react_.netcore_template.Application.Commons.Models;

namespace Test.Application.Commons.Employees.Queries
{
    [TestFixture]
    public class GetAllEmployeesWithPaginationQueryUnitTest
    {
        private Mock<IEmployeeRepository> _mockEmployeeRepo;
        private Mock<ICacheService> _mockCacheService;

        [OneTimeSetUp]
        public void Init()
        {
            _mockEmployeeRepo = new Mock<IEmployeeRepository>();
            _mockCacheService = new Mock<ICacheService>();
        }

        [SetUp]
        public void Reset()
        {
            _mockEmployeeRepo.Reset();
            _mockCacheService.Reset();
        }

        [Test]
        public async Task Handle_HasCache_ExpectMockAllEmployeeDtoWithPaginatedListQurey()
        { 
            var testResult = new PaginatedList<EmployeeDto>(new List<EmployeeDto>(), 1, 1, 5);
            _mockEmployeeRepo.Setup(m => m.GetPaginedListAsync(1, 5)).ReturnsAsync(testResult);
            var testQuery = new GetAllEmployeesWithPaginationQuery { PageNumber = 1, PageSize = 5 };
            _mockCacheService.Setup(m => m.GetCachedAsync<PaginatedList<EmployeeDto>>(testQuery.GetCacheKey()))
                .ReturnsAsync(testResult);
            var _handle = new GetAllEmployeesWithPaginationQueryHandler(_mockEmployeeRepo.Object, _mockCacheService.Object);

            var sut = await _handle.Handle(
                testQuery,
                new CancellationToken());

            _mockEmployeeRepo.Verify(m =>
            m.GetPaginedListAsync(It.Is<int>(x => x == 1), It.Is<int>(x => x == 5)), Times.Never());
            Assert.That(sut, Is.EqualTo(testResult));
        }

        [Test]
        public async Task Handle_NoHasCache_ExpectMockAllEmployeeDtoWithPaginatedListQurey()
        {
            var testResult = new PaginatedList<EmployeeDto>(new List<EmployeeDto>(), 1, 1, 5);
            _mockEmployeeRepo.Setup(m => m.GetPaginedListAsync(1, 5)).ReturnsAsync(testResult);
            _mockCacheService.Setup(m => m.GetCachedAsync<PaginatedList<EmployeeDto>>("test key"))
                .ReturnsAsync((PaginatedList<EmployeeDto>)null);

            var _handle = new GetAllEmployeesWithPaginationQueryHandler(_mockEmployeeRepo.Object, _mockCacheService.Object);

            var sut = await _handle.Handle(
                new GetAllEmployeesWithPaginationQuery { PageNumber = 1, PageSize = 5 },
                new CancellationToken());

            _mockEmployeeRepo.Verify(m =>
            m.GetPaginedListAsync(It.Is<int>(x => x == 1), It.Is<int>(x => x == 5)), Times.Once());
            Assert.That(sut, Is.EqualTo(testResult));
        }
    }
}
