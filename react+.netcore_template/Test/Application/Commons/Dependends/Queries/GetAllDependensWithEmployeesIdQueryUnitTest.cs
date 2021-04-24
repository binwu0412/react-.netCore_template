using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using react_.netcore_template.Application.Commons.Dependends.Queries;
using react_.netcore_template.Application.Commons.Interfaces;
using react_.netcore_template.Application.Commons.Models;
using react_.netcore_template.Domain.Entities;

namespace Test.Application.Commons.Dependends.Queries
{
    [TestFixture]
    public class GetAllDependensWithEmployeesIdQueryUnitTest
    {
        private Mock<IDependendRepository> _mockDependendRepository;
        private Mock<ICacheService> _mockCacheService;

        [OneTimeSetUp]
        public void Init()
        {
            _mockCacheService = new Mock<ICacheService>();
            _mockDependendRepository = new Mock<IDependendRepository>();
        }

        [SetUp]
        public void Reset()
        {
            _mockCacheService.Reset();
            _mockDependendRepository.Reset();
        }
        [Test]
        public async Task Handle_HandleCache_ExpectGetAllDependensWithEmployeesIdQuery()
        {
            var testResultElement = new DependendDto() { Id = 1, EmployeeId = 1 };
            var testResult = new List<DependendDto>();
            testResult.Add(testResultElement);
            _mockDependendRepository.Setup(m => m.GetAllDtoWithEmployeeIdAsync(1)).ReturnsAsync(testResult);

            var testQuery = new GetAllDependendsWithEmployeeIdQuery { EmployeeId = 1 };
            _mockCacheService.Setup(m => m.GetCachedAsync<List<DependendDto>>(testQuery.GetCacheKey()))
                .ReturnsAsync(testResult);
            var handle = new GetAllDependendsWithEmployeeIdQueryHandler(_mockDependendRepository.Object, _mockCacheService.Object);

            var sut = await handle.Handle(testQuery, new CancellationToken());

            _mockDependendRepository.Verify(m => m.GetAllDtoWithEmployeeIdAsync(It.Is<int>(x => x == 1)), Times.Never());
            Assert.That(sut, Is.EqualTo(testResult));
        }

        [Test]
        public async Task Handle_HandleNoCache_ExpectGetAllDependensWithEmployeesIdQuery()
        {
            var testResultElement = new DependendDto() { Id = 2, EmployeeId = 2 };
            var testResult = new List<DependendDto>();
            testResult.Add(testResultElement);
            _mockDependendRepository.Setup(m => m.GetAllDtoWithEmployeeIdAsync(2)).ReturnsAsync(testResult);

            var testQuery = new GetAllDependendsWithEmployeeIdQuery { EmployeeId = 2};
            _mockCacheService.Setup(m => m.GetCachedAsync<List<DependendDto>>(testQuery.GetCacheKey()))
                .ReturnsAsync((List<DependendDto>)null);
            var handle = new GetAllDependendsWithEmployeeIdQueryHandler(_mockDependendRepository.Object, _mockCacheService.Object);

            var sut = await handle.Handle(testQuery, new CancellationToken());

            _mockDependendRepository.Verify(m => m.GetAllDtoWithEmployeeIdAsync(It.Is<int>(x => x == 2)));
            Assert.That(sut, Is.EqualTo(testResult));
        }
    }
}
