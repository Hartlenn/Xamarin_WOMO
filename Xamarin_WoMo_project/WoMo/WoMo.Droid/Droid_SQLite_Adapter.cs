using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using WoMo.Logik;
using Xamarin.Forms;
using System.IO;
using WoMo.Droid;

[assembly: Dependency(typeof(Droid_SQLite_Adapter))]
namespace WoMo.Droid
{
    class Droid_SQLite_Adapter : SQLite_Adapter
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "WoMo.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            return new SQLite.SQLiteConnection(path);
        }
    }
}