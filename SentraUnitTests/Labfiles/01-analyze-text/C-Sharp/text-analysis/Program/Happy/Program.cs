using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace text_analysis.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void Main_ShouldAnalyzeTextFilesInReviewsFolder()
        {
            // Arrange
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.SetupGet(c => c["AIServicesEndpoint"]).Returns("https://fakeendpoint.cognitiveservices.azure.com/");
            mockConfig.SetupGet(c => c["AIServicesKey"]).Returns("fakekey");

            var mockClient = new Mock<TextAnalyticsClient>(Uri.Parse(mockConfig.Object["AIServicesEndpoint"]), new AzureKeyCredential(mockConfig.Object["AIServicesKey"]))
            {
                CallBase = true
            };

            mockClient.Setup(m => m.DetectLanguage(It.IsAny<string>())).Returns(new DetectedLanguage("English", "en"));
            mockClient.Setup(m => m.AnalyzeSentiment(It.IsAny<string>())).Returns(new DocumentSentiment(DocumentSentimentType.Neutral));
            mockClient.Setup(m => m.ExtractKeyPhrases(It.IsAny<string>())).Returns(new KeyPhraseCollection());
            mockClient.Setup(m => m.RecognizeEntities(It.IsAny<string>())).Returns(new CategorizedEntityCollection());
            mockClient.Setup(m => m.RecognizeLinkedEntities(It.IsAny<string>())).Returns(new LinkedEntityCollection());

            var mockConsole = new Mock<Console>();

            Environment.CurrentDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tests", "data");
            Directory.SetCurrentDirectory(Environment.CurrentDirectory);

            // Act
            Program.Main(null);

            // Assert
            mockConsole.Verify(c => c.WriteLine(It.IsAny<string>()), Times.AtLeastOnce);
            mockClient.VerifyAll();
        }
    }
}