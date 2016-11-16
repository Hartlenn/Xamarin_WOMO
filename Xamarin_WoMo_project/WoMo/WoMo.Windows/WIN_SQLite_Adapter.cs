using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using System.IO;
using Windows.Storage;

[assembly: Dependency(typeof(WoMo.Logik.SQLite_Adapter))]
namespace WoMo.Windows
{
    class WIN_SQLite_Adapter : WoMo.Logik.SQLite_Adapter
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "WoMo.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            return new SQLite.SQLiteConnection(path);
        }
    }
}
