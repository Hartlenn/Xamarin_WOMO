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
    class FileReadWrite_Universal : IFileReadWrite, IFileWriteForWindows
    {
        public Stream GetReadStream(string fileName)
        {
            var filePath = GetFilePath(fileName);
            return File.OpenRead(filePath);
        }

        public Stream GetWriteStream(string fileName)
        {
            throw new MyWindowsPhoneFileSystemException("Not possible with this method.");

            var filePath = GetFilePath(fileName);
            var stream = new FileStream(filePath, FileMode.CreateNew);
            return stream;
        }

        public bool FileExists(string fileName)
        {
            var filePath = GetFilePath(fileName);
            return File.Exists(filePath);
        }

        private string GetFilePath(string fileName)
        {
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, fileName);
            return path;
        }

        public async Task WriteFile(string filename, string text)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await localFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile, text);
        }
    }
}
