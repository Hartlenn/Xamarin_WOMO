using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using WoMo.Logik.FileReadWrite;
using WoMo.Droid;

[assembly: Dependency(typeof(FileReadWrite_Android))]
namespace WoMo.Droid
{
    public class FileReadWrite_Android : IFileReadWrite
    {
        public Stream GetReadStream(string fileName)
        {
            var filePath = GetFilePath(fileName);
            return File.OpenRead(filePath);
        }

        public Stream GetWriteStream(string fileName)
        {
            var filePath = GetFilePath(fileName);
            var stream = File.OpenWrite(filePath);
            return stream;
        }

        public bool FileExists(string fileName)
        {
            var filePath = GetFilePath(fileName);
            return File.Exists(filePath);
        }

        private string GetFilePath(string fileName)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(documentsPath, fileName);
        }
    }
}
