using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public interface IFileReader
    {
        string Read(string filename);
    }

    public class FileReader : IFileReader 
    {
        public string Read(string filename)
        {
            return System.IO.File.ReadAllText(filename);
        }
    }
}
