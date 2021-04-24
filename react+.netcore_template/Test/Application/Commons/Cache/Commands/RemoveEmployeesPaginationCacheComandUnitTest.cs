using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using NUnit.Framework;
using react_.netcore_template.Application.Commons.Cache.Commands;
using react_.netcore_template.Application.Commons.Interfaces;

namespace Test.Application.Commons.Cache.Commands
{
    [TestFixture]
    public class RemoveEmployeesPaginationCacheComandUnitTest
    {
        private Mock<ICacheService> _mockCacheService;

        [OneTimeSetUp]
        public void Init()
        {
            _mockCacheService = new Mock<ICacheService>();
        }

        [SetUp]
        public void Reset()
        {
            _mockCacheService.Reset();
        }
        [Test]
        public async Task Handle_handleRemoveByPatternExpectHaveBeenCalled()
        {
            var testCommand = new RemoveEmployeesWithPaginationCacheCommand("cache key");
            _mockCacheService.Setup(m => m.RemoveByPatternAsync(testCommand.CachePattern));

            var handler = new RemoveEmployeesWithPaginationCacheCommandHandler(_mockCacheService.Object);
            var sut = await handler.Handle(testCommand, new CancellationToken());

            _mockCacheService.Verify(m => m.RemoveByPatternAsync(It.Is<string>(x => x == testCommand.CachePattern)));
            Assert.That(sut, Is.EqualTo(Unit.Value));
        }
    }
}
