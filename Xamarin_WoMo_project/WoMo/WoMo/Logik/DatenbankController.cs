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
            database.CreateTable<DB_Bilder_Eintrag>();
            database.CreateTable<DB_Bilderliste>();
            database.CreateTable<DB_Checkliste>();
            database.CreateTable<DB_Checklisten_Eintrag>();
            database.CreateTable<DB_Standort>();
            database.CreateTable<DB_Stellplatz>();
            database.CreateTable<DB_Tagebuch>();
            database.CreateTable<DB_Tagebuch_Eintrag>();
            database.CreateTable<DB_TBEintrag_Standort>();
            database.CreateTable<DB_TBEintrag_Stellplatz>();
        }

        public DatenbankController getInstance()
        {
            return this;
        }

        public int insert(IListeneintrag eintrag)
        {
            if (eintrag is DB_Stellplatz)
            {

            }
            else if (eintrag is CLEintrag)
            {

            }
            else if(eintrag is TBEintrag)
            {

            }
            else if(eintrag is DB_Standort)
            {

            }
            lock (locker)
            {
                return database.Insert(eintrag);
            }
        }

        public bool update(IListeneintrag eintrag)
        {
            if (eintrag is DB_Stellplatz)
            {

            }
            else if (eintrag is CLEintrag)
            {

            }
            else if (eintrag is TBEintrag)
            {

            }
            else if (eintrag is DB_Standort)
            {

            }
            lock (locker)
            {
                return database.Update(eintrag) > 0;

            }
        }

        public bool delete(IListeneintrag eintrag)
        {
            if (eintrag is Stellplatz)
            {

            }
            else if (eintrag is CLEintrag)
            {

            }
            else if (eintrag is TBEintrag)
            {

            }
            else if (eintrag is Standort)
            {

            }
            lock (locker) {
                return database.Delete(eintrag) > 0;
            }
        }

        public IEnumerable<IListeneintrag> select(string Tabelle)
        {
            
        }
    }
}
