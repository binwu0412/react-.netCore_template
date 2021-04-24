using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using NUnit.Framework;
using react_.netcore_template.Application.Commons.Employees.Commands;
using react_.netcore_template.Application.Commons.Interfaces;
using react_.netcore_template.Application.Commons.Models;
using react_.netcore_template.Domain.Entities;

namespace Test.Application.Commons.Employees.Commands
{
    [TestFixture]
    public class AddEmployeeCommanUnitTest
    {
        private Mock<IEmployeeRepository> _mockEmployeeRepository;
        private Mock<IMediator> _mockMediator;

        [OneTimeSetUp]
        public void Init()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockMediator = new Mock<IMediator>();
        }

        [SetUp]
        public void Reset()
        {
            _mockMediator.Reset();
            _mockEmployeeRepository.Reset();
        }

        [Test]
        public async Task Hanlde_HandleAddEmployeeCommandExpectHaveBeenCalled()
        {
            var testEmployee = new Employee() { Name = "name", Title = "Title", Department = "Depart A" };
            _mockEmployeeRepository.Setup(m => m.AddAsync(testEmployee));

            var testCommand = new AddEmployeeCommand() { EmployeeDto = new EmployeeDto()
                { Name =  testEmployee.Name, Title = testEmployee.Title, Department =testEmployee.Department } };
            for (var i = 0; i < testCommand.GetFollowupRequests().Count; i++)
            {
                var request = testCommand.GetFollowupRequests()[i];
                _mockMediator.Setup(m => m.Send(request, new CancellationToken()));
            }
            
            var handler = new AddEmployeeCommandHandler(_mockEmployeeRepository.Object, _mockMediator.Object);
            var sut = await handler.Handle(testCommand, new CancellationToken());

            _mockEmployeeRepository.Verify(m => m.AddAsync(It.Is<Employee>(x => x.Department == testEmployee.Department && x.Name ==testEmployee.Name && x.Title == testEmployee.Title)));
            Assert.That(sut, Is.EqualTo(Unit.Value));
        }
    }
}
