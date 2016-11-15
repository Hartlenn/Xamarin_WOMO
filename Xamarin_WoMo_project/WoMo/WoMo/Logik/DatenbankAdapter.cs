using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.IO;
using WoMo.Logik.Listeneinträge;

namespace WoMo.Logik
{
    class DatenbankAdapter
    {
        static object locker = new object();
        SQLiteConnection database;
        private static DatenbankAdapter singleton;

    //device specific maybe not working?
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

        private DatenbankAdapter()
        {
            initialisiereDatenbank();
        }

        // Singleton
        public static DatenbankAdapter getInstance()
        {
            if (singleton == null)
                singleton = new DatenbankAdapter();

            return singleton;
        }

        public bool initialisiereDatenbank()
        {
            try
            {
                database = new SQLiteConnection(databasePath);
            }
            catch
            {
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

        public DatenbankAdapter getInstance()
        {
            return this;
        }

        public int insert(IListeneintrag eintrag)
        {
            lock (locker)
            {
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
            lock (locker)
            {
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
            Tabelle = Tabelle.ToLower();

            Listenklasse<IListeneintrag> list = new Listenklasse<IListeneintrag>();
            if (Tabelle.Equals("stellplatz"))
            {
                List<Stellplatz> collection;
                collection = database.Query<Stellplatz>("SELECT * FROM [Stellplatz]");
                list.addRange(collection);
                
            }
            else if (Tabelle.Equals("cleintrag"))
            {
                List<CLEintrag> collection;
                collection = database.Query<CLEintrag>("SELECT * FROM [CLEintrag]");
                list.addRange(collection);
            }
            else if (Tabelle.Equals("tbeintrag"))
            {
                List<TbEintrag> collection;
                collection = database.Query<TbEintrag>("SELECT * FROM [TbEintrag]");
                list.addRange(collection);
            }
            else if (Tabelle.Equals("bildereintrag"))
            {
                List<BilderEintrag> collection;
                collection = database.Query<BilderEintrag>("SELECT * FROM [BilderEintrag]");
                list.addRange(collection);
            }
            else
            {
                return null;
            }

            return list;
        }
    }
}

