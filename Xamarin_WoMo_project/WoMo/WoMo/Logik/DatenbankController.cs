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
        //needs to be filled later(device specific)
        private string databasePath;

        public DatenbankController()
        {
            initialisiereDatenbank();
        }

        public bool initialisiereDatenbank()
        {
            try{
                database = new SQLiteConnection(databasePath);
            }
            catch{
                return false;
            }
            return true;
        }

        public void erstelleObjekte()
        {
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

        public DatenbankController getInstance()
        {
            return this;
        }

        public int insert(IListeneintrag eintrag)
        {
            lock (locker)
            {
                return database.Insert(eintrag);
            }
        }

        public bool update(IListeneintrag eintrag)
        {
            lock (locker)
            {
                return database.Update(eintrag) > 0;

            }
        }

        public bool delete(IListeneintrag eintrag)
        {
            lock (locker) {
                return database.Delete(eintrag) > 0;
            }
        }

        public IListeneintrag select(String Tabelle)
        {
            throw new NotImplementedException();
        }
    }
}
