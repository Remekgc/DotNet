using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            VideoService service = new VideoService();
            string title = service.ReadVideoTitle();
        }
    }
}
