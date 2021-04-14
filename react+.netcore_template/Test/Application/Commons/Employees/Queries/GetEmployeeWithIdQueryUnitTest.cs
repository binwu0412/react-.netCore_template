using System;
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
    public class GetEmployeeWithIdQueryUnitTest
    {

        [Test]
        public async Task Handle_HasCache_ExpectMockEmployeeDtoWithId33()
        {
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository.Setup(m => m.GetDtoAsync(33)).ReturnsAsync(new EmployeeDto() { Id = 33 });
            var mockCacheService = new Mock<ICacheService>();
            mockCacheService.Setup(m => m.GetCachedAsync<EmployeeDto>("getEmployeeWithIdQuery" + 33))
                .ReturnsAsync(new EmployeeDto() { Id = 33 });
            var _handler = new GetEmployeeWithIdQueryHandler(mockEmployeeRepository.Object, mockCacheService.Object);

            var sut = await _handler.Handle(new GetEmployeeWithIdQuery() { Id = 33 }, new CancellationToken());

            mockEmployeeRepository.Verify(m => m.GetDtoAsync(It.Is<int>(x => x == 33)),Times.Once());
            Assert.That(sut.Id, Is.EqualTo(33));
        }

        [Test]
        public async Task Handle_HasNoCache_ExpectMockEmpoyeeDtoWothId34()
        {
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository.Setup(m => m.GetDtoAsync(34)).ReturnsAsync(new EmployeeDto() { Id = 34 });
            var mockCacheService = new Mock<ICacheService>();
            mockCacheService.Setup(m => m.GetCachedAsync<EmployeeDto>("test key"))
                .ReturnsAsync((EmployeeDto)null);
            var _handler = new GetEmployeeWithIdQueryHandler(mockEmployeeRepository.Object, mockCacheService.Object);

            var sut = await _handler.Handle(new GetEmployeeWithIdQuery() { Id = 34 }, new CancellationToken());

            mockEmployeeRepository.Verify(m => m.GetDtoAsync(It.Is<int>(x => x == 34)),Times.Once());
            Assert.That(sut.Id, Is.EqualTo(34));
        }
    }

}
