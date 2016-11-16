using System;
using System.IO;
using Xamarin.Forms;
using WoMo.Logik;

[assembly: Dependency (typeof(SQLite_Adapter))]
namespace WoMo.iOS
{
    class IOS_SQLite_Adapter : SQLite_Adapter
    {
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "WoMo.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);
            return new SQLite.SQLiteConnection(path);
        }
    }
}