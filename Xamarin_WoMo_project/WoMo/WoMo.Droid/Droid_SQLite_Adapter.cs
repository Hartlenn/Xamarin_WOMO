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
using WoMo.Logik;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Adapter))]
namespace WoMo.Droid
{
    class Droid_SQLite_Adapter
    {
        
        //device specific maybe not working?
             /* string databasePath
              {
                  get
                  {
                      var sqlliteFilename = "WoMo.db3";
      #if __IOS__
                      string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                      string libaryPath = Path.Combine(documentsPath, "..", "Libary");
                      var path = Path.Combine(libaryPath, sqlliteFilename);
      #else
      #if __ANDROID__
                          string documentsPath = Environment.getFolderPath(Environment.SpecialFolder.Personal);
                          var path = Path.Combine(documentsPath, sqlliteFilename);
      #else
                      var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqlliteFilename);
      #endif
      #endif
                      return path;
                  }
              }*/
    }
}