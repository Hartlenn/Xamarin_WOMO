using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.FileReadWrite
{
    public interface IFileWriteForWindows
    {

        Task WriteFile(string filename, string text);
    }
}
