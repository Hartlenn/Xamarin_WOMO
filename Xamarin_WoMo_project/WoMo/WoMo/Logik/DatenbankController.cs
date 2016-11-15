using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using WoMo.Logik.Database;
using WoMo.Logik.Listeneinträge;
using System.Linq;


namespace WoMo.Logik
{
    class DatenbankController
    {
        static object locker = new object();
        SQLiteConnection database;
        //needs to be filled later(device specific)
       string databasePath
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
        }

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
            if (eintrag is Stellplatz)
            {
                throw new NotImplementedException();
            }
            else if (eintrag is CLEintrag)
            {
                throw new NotImplementedException();
            }
            else if(eintrag is TbEintrag)
            {
                throw new NotImplementedException();
            }
            else if(eintrag is Standort)
            {
                throw new NotImplementedException();
            }
            lock (locker)
            {
                return database.Insert(eintrag);
            }
        }

        public bool update(IListeneintrag eintrag)
        {
            lock (locker) { 
                if (eintrag is DB_Stellplatz)
                {
                    throw new NotImplementedException();
                }
                else if (eintrag is CLEintrag)
                {
                    throw new NotImplementedException()
                }
                else if (eintrag is TbEintrag)
                {
                    throw new NotImplementedException();
                }
                else if (eintrag is DB_Standort)
                {
                    throw new NotImplementedException();
                }
            }

            return false;
        }

        public bool delete(IListeneintrag eintrag)
        {
            lock (locker)
            {
                if (eintrag is Stellplatz)
                {
                    throw new NotImplementedException();
                }
                else if (eintrag is CLEintrag)
                {
                    throw new NotImplementedException();
                }
                else if (eintrag is TbEintrag)
                {
                    List<DB_TBEintrag_Standort> standorte = database.Query<DB_TBEintrag_Standort>("SELECT * FROM [DB_TBEintrag_Standort] WHERE [TBEintragID] = ?", eintrag.Id);
                    foreach(DB_TBEintrag_Standort pos in standorte)
                    {
                        database.Delete<DB_TBEintrag_Standort>(pos.Id);
                    }
                    List<DB_TBEintrag_Stellplatz> plaetze = database.Query<DB_TBEintrag_Stellplatz>("SELECT * FROM [DB_TBEintrag_Stellplatz] WHERE [TBEintragID] = ?", eintrag.Id);
                    foreach(DB_TBEintrag_Stellplatz platz in plaetze)
                    {
                        database.Delete<DB_TBEintrag_Stellplatz>(platz.Id);
                    }
                    return database.Delete<DB_Tagebuch_Eintrag>(eintrag.Id) > 0;
                }
                else if (eintrag is Standort)
                {
                    throw new NotImplementedException();
                }
            }

            return false;
        }

        public IEnumerable<IListeneintrag> select(string Tabelle)
        {
            throw new NotImplementedException();
            return null;
        }
    }
}
