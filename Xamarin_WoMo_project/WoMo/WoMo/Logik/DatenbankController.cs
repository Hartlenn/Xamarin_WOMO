using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoMo.Logik;
using WoMo.Logik.Database;

namespace WoMo.Logik
{
    class DatenbankController
    {
        static object locker = new object();
        SQLiteConnection database;
        private string databasePath;
        public DatenbankController()
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Bilder_Eintrag>();
            database.CreateTable<Bilderliste>();
            database.CreateTable<Checkliste>();
            database.CreateTable<Checklisten_Eintrag>();
            database.CreateTable<Standort>();
            database.CreateTable<Stellplatz>();
            database.CreateTable<Tagebuch>();
            database.CreateTable<Tagebuch_Eintrag>();
            database.CreateTable<TBEintrag_Standort>();
            database.CreateTable<TBEintrag_Stellplatz>();
        }

        public IListenklasse<T> GetItems()
        {
            lock (locker)
            {
                return (from i in database.Table<TBEintrag_Stellplatz>() select i).ToList();
            }
        }
    }
}
