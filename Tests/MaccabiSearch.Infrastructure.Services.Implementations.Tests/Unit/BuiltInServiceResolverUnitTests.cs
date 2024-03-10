using MaccabiSearch.Common.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace MaccabiSearch.Infrastructure.Services.Implementations.Tests
{
    [TestClass]
    public class BuiltInServiceResolverUnitTests
    {
        private readonly IServiceCollection services;

        public BuiltInServiceResolverUnitTests()
        {
            services = new ServiceCollection();
        }

        [TestMethod]
        public void Resolve_ReturnsRequestedService_Successfully()
        {
            // Arrange
            services
                .AddSingleton<IMockService, MockService>();

            var target = new BuiltInServiceResolver(services.BuildServiceProvider());

            // Act
            var actual = target.Resolve<IMockService>();

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType<MockService>(actual);
            Assert.AreEqual(Constants.ItsWorking, actual.Test());
        }

        [TestMethod]
        public void Resolve_ReturnsRequestedByKeyService_Successfully()
        {
            // Arrange
            services
                .AddKeyedSingleton<IMockService, MockService>(Constants.MockServiceKey);

            var target = new BuiltInServiceResolver(services.BuildServiceProvider());

            // Act
            var actual = target.Resolve<IMockService>(Constants.MockServiceKey);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType<MockService>(actual);
            Assert.AreEqual(Constants.ItsWorking, actual.Test());
        }

        [TestMethod]
        public void Resolve_ReturnsDefaultServiceRequestedByKey_Successfully()
        {
            // Arrange
            services
                .AddSingleton<IMockService, MockService>();

            var target = new BuiltInServiceResolver(services.BuildServiceProvider());

            // Act
            var actual = target.Resolve<IMockService>(Constants.MockServiceKey);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType<MockService>(actual);
            Assert.AreEqual(Constants.ItsWorking, actual.Test());
        }
    }
}