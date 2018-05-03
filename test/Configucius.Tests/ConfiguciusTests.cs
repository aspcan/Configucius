using System;
using Configucius.Core;
using Configucius.Provider.SQL;
using FluentAssertions;
using NUnit.Framework;

namespace Configucius.Tests
{
    [TestFixture]
    public class ConfiguciusTests
    {
        //TODO: All tests should be completed.

        private IConfigucius _configucius;

        [SetUp]
        public void Initialize()
        {
            string domain = "Product";
            string environment = "Stage";
            string connectionString = "";

            _configucius = new ConfiguciusClient(new SqlConfigRepository(connectionString: connectionString),
                domain: domain, environment: environment, refreshTime: TimeSpan.FromMinutes(2));
        }

        [Test]
        public void GetValue_WhenGivenAKey_ShouldReturnConvertedValueAsAString()
        {
            //Arrange
            string key = "Demo";
            string expectedValue = "Hello World";

            //Act
            string value = _configucius.GetValue<string>(key);

            //Assert
            value.Should().Be(expectedValue);
        }
    }
}