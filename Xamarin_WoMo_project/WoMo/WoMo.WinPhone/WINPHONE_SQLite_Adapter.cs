using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;
using Windows.Storage;
using Xamarin.Forms;
using WoMo.Logik;
using WoMo.WinPhone;

[assembly: Dependency(typeof(WINPHONE_SQLite_Adapter))]
namespace WoMo.WinPhone
{
    class WINPHONE_SQLite_Adapter : SQLite_Adapter
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "WoMo.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            return new SQLite.SQLiteConnection(path);
        }
    }
}
