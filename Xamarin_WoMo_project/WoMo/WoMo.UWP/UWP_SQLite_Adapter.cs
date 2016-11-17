using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using System.IO;
using Windows.Storage;
using WoMo.Logik;
using WoMo.UWP;

[assembly: Dependency (typeof (UWP_SQLite_Adapter))]
namespace WoMo.UWP
{
    class UWP_SQLite_Adapter : SQLite_Adapter
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "WoMo.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            return new SQLite.SQLiteConnection(path);
        }
    }
}
