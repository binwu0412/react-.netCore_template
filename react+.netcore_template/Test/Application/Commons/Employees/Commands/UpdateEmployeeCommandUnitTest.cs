using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using NUnit.Framework;
using react_.netcore_template.Application.Commons.Employees.Commands;
using react_.netcore_template.Application.Commons.Interfaces;
using react_.netcore_template.Application.Commons.Models;
using react_.netcore_template.Domain.Bus;
using react_.netcore_template.Domain.Entities;

namespace Test.Application.Commons.Employees.Commands
{
    [TestFixture]
    public class UpdateEmployeeCommandUnitTest
    {
        private Mock<IMediator> _mockMediator;
        private Mock<IEmployeeRepository> _mockEmployeeRepository;
        private Mock<IDependendRepository> _mockDependendRepository;
        private Mock<IEventBus> _mockEventBus;

        [OneTimeSetUp]
        public void Init()
        {
            _mockDependendRepository = new Mock<IDependendRepository>();
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockEventBus = new Mock<IEventBus>();
            _mockMediator = new Mock<IMediator>();
        }

        [SetUp]
        public void Reset()
        {
            _mockMediator.Reset();
            _mockDependendRepository.Reset();
            _mockEventBus.Reset();
            _mockEmployeeRepository.Reset();
        }

        [Test]
        public async Task Update_UpdateEmployeeAsyncExpectHaveBeenCalled()
        {
            var testEmployee = new Employee() { Id = 1, Name = "name", Title = "Title"};
            _mockEmployeeRepository.Setup(m => m.UpdateAsync(testEmployee));
            var testUpdateCommand = new UpdateEmployeeCommand() { EmployeeDto = new EmployeeDto()
                { Id = testEmployee.Id, Name = testEmployee.Name, Title = testEmployee.Title } };
            for (var i = 0; i < testUpdateCommand.GetFollowupRequests().Count; i++)
            {
                var request = testUpdateCommand.GetFollowupRequests()[i];
                _mockMediator.Setup(m => m.Send(request, new CancellationToken()));
            }

            var update = new UpdateEmployeeCommandHandler(
                _mockMediator.Object, _mockEmployeeRepository.Object, _mockDependendRepository.Object, _mockEventBus.Object);
            await update.(testUpdateCommand, new CancellationToken());


            _mockEmployeeRepository.Verify(m => m.UpdateAsync(It.Is<Employee>(x =>
            x.Id == testEmployee.Id && x.Name == testEmployee.Name && x.Title == testEmployee.Title)));

            

        }

        [Test]
        public async Task Update_UpdateDependendsAsyncExpectHaveBeenCalled()
        {
            var testDependend = new Dependend() { Id = 1, EmployeeId = 1 };
            var testUpdate = new List<Dependend>();
            testUpdate.Add(testDependend);
            _mockDependendRepository.Setup(m => m.UpdateAndDeleteRangeAsync(testUpdate, 1));
            
        }
    }
}
