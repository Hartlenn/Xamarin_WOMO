using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using WoMo.Logik.FileReadWrite;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileReadWrite_Universal))]
namespace WoMo.Logik.FileReadWrite
{
    class FileReadWrite_Universal : IFileReadWrite
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
            var sqliteFilename = "WoMo.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            return path;
        }
    }
}
