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
            database.CreateTable<BilderEintrag>();
            database.CreateTable<CLEintrag>();
            database.CreateTable<Standort>();
            database.CreateTable<Stellplatz>();
            database.CreateTable<TbEintrag>();
        }

        public DatenbankController getInstance()
        {
            return this;
        }

        public int insert(IListeneintrag eintrag)
        {
            lock (locker) { 
                if (eintrag is Stellplatz)
                {
                    return database.Insert((Stellplatz)eintrag);
                }
                else if (eintrag is CLEintrag)
                {
                    return database.Insert((CLEintrag)eintrag);
                }
                else if (eintrag is TbEintrag)
                {
                    return database.Insert((TbEintrag)eintrag);
                }
                else if (eintrag is BilderEintrag)
                {
                    return database.Insert((BilderEintrag)eintrag);
                }
                else
                    return 0;
            }
        }

        public bool update(IListeneintrag eintrag)
        {
            lock (locker) {
                if (eintrag is Stellplatz)
                {
                    return database.Update((Stellplatz)eintrag) > 0;
                }
                else if (eintrag is CLEintrag)
                {
                    return database.Update((CLEintrag)eintrag) > 0;
                }
                else if (eintrag is TbEintrag)
                {
                    return database.Update((TbEintrag)eintrag) > 0;
                }
                else if (eintrag is BilderEintrag)
                {
                    return database.Update((BilderEintrag)eintrag) > 0;
                }
                else
                    return false;
            }
        }

        public bool delete(IListeneintrag eintrag)
        {
            lock (locker)
            {
                if (eintrag is Stellplatz)
                {
                    return database.Delete((Stellplatz)eintrag) > 0;
                }
                else if (eintrag is CLEintrag)
                {
                    return database.Delete((CLEintrag)eintrag) > 0;
                }
                else if (eintrag is TbEintrag)
                {
                    return database.Delete((TbEintrag)eintrag) > 0;
                }
                else if (eintrag is BilderEintrag)
                {
                    return database.Delete((BilderEintrag)eintrag) > 0;
                }
                else
                    return false;
            }
        }

        public Listenklasse<IListeneintrag> select(string Tabelle)
        {
            Listenklasse<IListeneintrag> list = new Listenklasse<IListeneintrag>();
            if (Tabelle.Equals("Stellplatz"))
            {
                list = database.Query<Stellplatz>("SELECT * FROM [Stellplatz]");
            }
            else if (Tabelle.Equals("CLEintrag"))
            {
                list = database.Query<CLEintrag>("SELECT * FROM [CLEintrag]");
            }
            else if (Tabelle.Equals("TbEintrag"))
            {
                list = database.Query<TbEintrag>("SELECT * FROM [TbEintrag]");
            }
            else if (Tabelle.Equals("BilderEintrag"))
            {
                list = database.Query<BilderEintrag>("SELECT * FROM [BilderEintrag]");
            }
            else
            {
                return null;
            }

            return list;
        }
    }
}
