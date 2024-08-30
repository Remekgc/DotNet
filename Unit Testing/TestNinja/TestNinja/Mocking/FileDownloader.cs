using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    internal class FileDownloader : IFileDownloader
    {
        public void DownloadFile(string url, string path)
        {
            WebClient client = new WebClient();

            client.DownloadFile(url, path);
        }
    }
}
