```csharp
using NUnit.Framework;
using Moq;
using System;
using System.IO;

namespace text_analysis.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        private Mock<IConfiguration> _mockConfig;
        private Mock<TextAnalyticsClient> _mockClient;
        private string _testFolderPath;

        [SetUp]
        public void Setup()
        {
            _mockConfig = new Mock<IConfiguration>();
            _mockClient = new Mock<TextAnalyticsClient>(Uri.Parse("https://example.com"), new AzureKeyCredential("key"));
            _testFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestFiles");
            Directory.CreateDirectory(_testFolderPath);
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete(_testFolderPath, true);
        }

        [Test]
        public void Main_WithEmptyFolder_ShouldHandleCorrectly()
        {
            // Arrange
            var folderPath = Path.Combine(_testFolderPath, "empty");
            Directory.CreateDirectory(folderPath);

            _mockConfig.SetupGet(c => c["AIServicesEndpoint"]).Returns("https://example.com");
            _mockConfig.SetupGet(c => c["AIServicesKey"]).Returns("key");

            var program = new Program();

            // Act & Assert
            Assert.DoesNotThrow(() => program.Main(new string[] { }));
        }

        [Test]
        public void Main_WithSingleFile_ShouldAnalyzeText()
        {
            // Arrange
            var filePath = Path.Combine(_testFolderPath, "single.txt");
            File.WriteAllText(filePath, "This is a test.");

            _mockConfig.SetupGet(c => c["AIServicesEndpoint"]).Returns("https://example.com");
            _mockConfig.SetupGet(c => c["AIServicesKey"]).Returns("key");

            _mockClient.Setup(c => c.DetectLanguage(It.IsAny<string>())).Returns(new DetectedLanguage("English", "en"));
            _mockClient.Setup(c => c.AnalyzeSentiment(It.IsAny<string>())).Returns(new DocumentSentiment(SentimentType.Neutral));
            _mockClient.Setup(c => c.ExtractKeyPhrases(It.IsAny<string>())).Returns(new KeyPhraseCollection(new List<string> { "test" }));
            _mockClient.Setup(c => c.RecognizeEntities(It.IsAny<string>())).Returns(new CategorizedEntityCollection(new List<CategorizedEntity>()));
            _mockClient.Setup(c => c.RecognizeLinkedEntities(It.IsAny<string>())).Returns(new LinkedEntityCollection(new List<LinkedEntity>()));

            var program = new Program();

            // Act
            program.Main(new string[] { });

            // Assert
            _mockClient.Verify(c => c.DetectLanguage(It.IsAny<string>()), Times.Once);
            _mockClient.Verify(c => c.AnalyzeSentiment(It.IsAny<string>()), Times.Once);
            _mockClient.Verify(c => c.ExtractKeyPhrases(It.IsAny<string>()), Times.Once);
            _mockClient.Verify(c => c.RecognizeEntities(It.IsAny<string>()), Times.Once);
            _mockClient.Verify(c => c.RecognizeLinkedEntities(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void Main_WithMultipleFiles_ShouldAnalyzeEachFile()
        {
            // Arrange
            var file1Path = Path.Combine(_testFolderPath, "file1.txt");
            var file2Path = Path.Combine(_testFolderPath, "file2.txt");
            File.WriteAllText(file1Path, "This is file 1.");
            File.WriteAllText(file2Path, "This is file 2.");

            _mockConfig.SetupGet(c => c["AIServicesEndpoint"]).Returns("https://example.com");
            _mockConfig.SetupGet(c => c["AIServicesKey"]).Returns("key");

            _mockClient.Setup(c => c.DetectLanguage(It.IsAny<string>())).Returns(new DetectedLanguage("English", "en"));
            _mockClient.Setup(c => c.AnalyzeSentiment(It.IsAny<string>())).Returns(new DocumentSentiment(SentimentType.Neutral));
            _mockClient.Setup(c => c.ExtractKeyPhrases(It.IsAny<string>())).Returns(new KeyPhraseCollection(new List<string> { "test" }));
            _mockClient.Setup(c => c.RecognizeEntities(It.IsAny<string>())).Returns(new CategorizedEntityCollection(new List<CategorizedEntity>()));
            _mockClient.Setup(c => c.RecognizeLinkedEntities(It.IsAny<string>())).Returns(new LinkedEntityCollection(new List<LinkedEntity>()));

            var program = new Program();

            // Act
            program.Main(new string[] { });

            // Assert
            _mockClient.Verify(c => c.DetectLanguage(It.IsAny<string>()), Times.Exactly(2));
            _mockClient.Verify(c => c.AnalyzeSentiment(It.IsAny<string>()), Times.Exactly(2));
            _mockClient.Verify(c => c.ExtractKeyPhrases(It.IsAny<string>()), Times.Exactly(2));
            _mockClient.Verify(c => c.RecognizeEntities(It.IsAny