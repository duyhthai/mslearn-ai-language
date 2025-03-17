using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace text_analysis.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestDetectLanguage_ThrowsException_WhenNoConfiguration()
        {
            // Arrange
            var mockConfig = new Dictionary<string, string>();
            var mockBuilder = new Mock<IConfigurationBuilder>().Setup(b => b.AddJsonFile(It.IsAny<string>())).Returns(mockBuilder.Object);
            var mockRoot = new Mock<IConfigurationRoot>();
            mockBuilder.Setup(b => b.Build()).Returns(mockRoot.Object);

            IConfiguration configuration = mockRoot.Object;
            string aiSvcEndpoint = null;
            string aiSvcKey = null;

            AzureKeyCredential credentials = new AzureKeyCredential(aiSvcKey);
            Uri endpoint = new Uri(aiSvcEndpoint);
            TextAnalyticsClient aiClient = new TextAnalyticsClient(endpoint, credentials);

            // Act & Assert
            aiClient.DetectLanguage("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAnalyzeSentiment_ThrowsException_WhenEmptyText()
        {
            // Arrange
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();
            string aiSvcEndpoint = configuration["AIServicesEndpoint"];
            string aiSvcKey = configuration["AIServicesKey"];

            AzureKeyCredential credentials = new AzureKeyCredential(aiSvcKey);
            Uri endpoint = new Uri(aiSvcEndpoint);
            TextAnalyticsClient aiClient = new TextAnalyticsClient(endpoint, credentials);

            // Act & Assert
            aiClient.AnalyzeSentiment("");
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TestExtractKeyPhrases_ThrowsException_WhenFileDoesNotExist()
        {
            // Arrange
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();
            string aiSvcEndpoint = configuration["AIServicesEndpoint"];
            string aiSvcKey = configuration["AIServicesKey"];

            AzureKeyCredential credentials = new AzureKeyCredential(aiSvcKey);
            Uri endpoint = new Uri(aiSvcEndpoint);
            TextAnalyticsClient aiClient = new TextAnalyticsClient(endpoint, credentials);

            var filePath = Path.GetFullPath("./nonexistentfile.txt");
            StreamReader sr = File.OpenText(filePath);
            var text = sr.ReadToEnd();
            sr.Close();

            // Act & Assert
            aiClient.ExtractKeyPhrases(text);
        }
    }
}