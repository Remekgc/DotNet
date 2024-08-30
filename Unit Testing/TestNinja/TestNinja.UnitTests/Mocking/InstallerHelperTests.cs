using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    internal class InstallerHelperTests
    {
        Mock<IFileDownloader> fileDownloader;
        InstallerHelper installerHelper;

        [SetUp]
        public void SetUp()
        {
            fileDownloader = new Mock<IFileDownloader>();
            installerHelper = new InstallerHelper(fileDownloader.Object);
        }

        [Test]
        public void DownloadInstaller_DownloadSucceeds_ReturnsTrue()
        {
            this.fileDownloader.Setup(fileDownloader => fileDownloader.DownloadFile(It.IsAny<string>(), It.IsAny<string>()));

            bool result = installerHelper.DownloadInstaller("customerName", "installerName");

            Assert.That(result, Is.True);
        }

        [Test]
        public void DownloadInstaller_DownloadFails_ReturnsFalse()
        {
            this.fileDownloader.Setup(fileDownloader => fileDownloader.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();

            bool result = installerHelper.DownloadInstaller("customerName", "installerName");

            Assert.That(result, Is.False);
        }
    }
}
