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
    public class GetSearchEmployeesWithPaginationQureyUnitTest
    {

        [Test]
        public async Task Handle_GetSearchEmployeesWithPagiationQueryExpectReturnNewEmployeeList()
        {
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            var mockTestResult = new PaginatedList<EmployeeDto>(new List<EmployeeDto>(), 0, 0, 10);
            mockEmployeeRepository.Setup(m => m.GetPaginatedSearchListAsync(10, "search"))
                .ReturnsAsync(mockTestResult);
            var handler = new GetSearchEmployeesWithPaginationQueryHandler(mockEmployeeRepository.Object);

            var sut = await handler.Handle
            (
                new GetSearchEmployeesWithPaginationQuery() { SearchString = "search", PageSize = 10 },
                new CancellationToken()
            );

            mockEmployeeRepository.Verify(m =>
            m.GetPaginatedSearchListAsync(It.Is<int>(x => x == 10), It.Is<string>(x => x == "search")), Times.Once());
            Assert.That(sut, Is.EqualTo(mockTestResult));
        }
    }
}
