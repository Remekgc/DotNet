using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace TestNinja.Mocking.UnitTests
{
    [TestFixture]
    internal class VideoServiceTests
    {
        VideoService videoService;
        Mock<IFileReader> mockFileReader;
        Mock<IVideoRepository> videoRepository;

        [SetUp]
        public void SetUp()
        {
            this.mockFileReader = new Mock<IFileReader>();
            this.videoRepository = new Mock<IVideoRepository>();
            this.videoService = new VideoService(mockFileReader.Object, videoRepository.Object);
        }

        [TearDown]
        public void TearDown()
        {
            this.videoService = null;
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            mockFileReader.Setup(fr => fr.Read("video.txt")).Returns(""); // Use mocking only for external dependencies.

            string result = videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnEmptyString()
        {
            videoRepository.Setup(vr => vr.GetUnprocessedVideos()).Returns(new List<Video>());

            string result = videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AFewUnprocessedVideos_ReturnStringWithIDsOfUnprocessedVideos()
        {
            videoRepository.Setup(vr => vr.GetUnprocessedVideos()).Returns(new List<Video>
            {
                new Video { Id = 1 },
                new Video { Id = 2 },
                new Video { Id = 3 }
            });

            string result = videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}
