using System.Net;

namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        IFileDownloader fileDownloader;
        private string setupDestinationFile;

        public InstallerHelper(IFileDownloader fileDownloader)
        {
            this.fileDownloader = fileDownloader;
        }

        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
                fileDownloader.DownloadFile(
                    string.Format("http://example.com/{0}/{1}",
                        customerName,
                        installerName),
                    setupDestinationFile);

                return true;
            }
            catch (WebException)
            {
                return false; 
            }
        }
    }
}