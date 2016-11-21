using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WoMo.Logik.FileReadWrite
{
    interface IFileReadWrite
    {
        Stream GetWriteStream(string fileName);
        Stream GetReadStream(string fileName);
        bool FileExists(string fileName);
    }
}
