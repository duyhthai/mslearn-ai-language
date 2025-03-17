using NUnit.Framework;
using System;

namespace text_analysis.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void Main_ThrowsException_WhenAppSettingsMissing()
        {
            // Arrange
            Environment.SetEnvironmentVariable("AIServicesEndpoint", null);
            Environment.SetEnvironmentVariable("AIServicesKey", null);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => Program.Main(new string[] { }));
        }

        [Test]
        public void Main_ThrowsException_WhenInvalidEndpointProvided()
        {
            // Arrange
            Environment.SetEnvironmentVariable("AIServicesEndpoint", "invalid_endpoint");
            Environment.SetEnvironmentVariable("AIServicesKey", "valid_key");

            // Act & Assert
            Assert.Throws<Azure.RequestFailedException>(() => Program.Main(new string[] { }));
        }

        [Test]
        public void Main_ThrowsException_WhenInvalidKeyProvided()
        {
            // Arrange
            Environment.SetEnvironmentVariable("AIServicesEndpoint", "https://textanalytics.cognitiveservices.azure.com/");
            Environment.SetEnvironmentVariable("AIServicesKey", "invalid_key");

            // Act & Assert
            Assert.Throws<Azure.RequestFailedException>(() => Program.Main(new string[] { }));
        }
    }
}