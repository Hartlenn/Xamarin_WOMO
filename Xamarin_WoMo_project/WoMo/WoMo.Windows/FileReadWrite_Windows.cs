using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;
using WoMo.Logik.FileReadWrite;
using WoMo.Windows;

[assembly: Dependency(typeof(FileReadWrite_Windows))]
namespace WoMo.Windows
{
    public class FileReadWrite_Windows : IFileReadWrite
    {
        public Stream GetReadStream(string fileName)
        {
            throw new MyWindowsPhoneFileSystemException("Not possible with this method.");
        }

        public Stream GetWriteStream(string fileName)
        {
            throw new MyWindowsPhoneFileSystemException("Not possible with this method.");
        }

        public bool FileExists(string fileName)
        {
            throw new MyWindowsPhoneFileSystemException("Not possible with this method.");
        }

        private string GetFilePath(string fileName)
        {
            throw new MyWindowsPhoneFileSystemException("Not possible with this method.");
        }

        public static async Task WriteFile(string filename, string text)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await localFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile, text);
        }
    }

    
}
