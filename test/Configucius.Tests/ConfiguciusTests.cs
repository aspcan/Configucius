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

        private IConfigucius _Configucius;

        [SetUp]
        public void Initialize()
        {

            _Configucius = new ConfiguciusClient(new SqlConfigRepository(), TimeSpan.FromMinutes(2));
        }

        [Test]
        public void GetValue_WhenGivenAKey_ShouldReturnConvertedValueAsAString()
        {
            //Arrange
            string key = "Demo";
            string expectedValue = "Hello World";

            //Act
            string value = _Configucius.GetValue<string>(key);

            //Assert
            value.Should().Be(expectedValue);
        }
    }
}