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

        public DatenbankAdapter()
        {
            initialisiereDatenbank();
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

        public void work()
        {
            //in funktionen in queue werfen
            //queue abhandeln
            //extra task
            //aktuelle methoden auf private setzten neue eingabe(für buffer)
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
            Listenklasse<IListeneintrag> list = new Listenklasse<IListeneintrag>();
            if (Tabelle.Equals("Stellplatz"))
            {
                List<Stellplatz> collection;
                collection = database.Query<Stellplatz>("SELECT * FROM [Stellplatz]");
                foreach (Stellplatz item in collection)
                {
                    list.add((IListeneintrag)item);
                }
            }
            else if (Tabelle.Equals("CLEintrag"))
            {
                List<CLEintrag> collection;
                collection = database.Query<CLEintrag>("SELECT * FROM [CLEintrag]");
                foreach (CLEintrag item in collection)
                {
                    list.add((IListeneintrag)item);
                }
            }
            else if (Tabelle.Equals("TbEintrag"))
            {
                List<TbEintrag> collection;
                collection = database.Query<TbEintrag>("SELECT * FROM [TbEintrag]");
                foreach (TbEintrag item in collection)
                {
                    list.add((IListeneintrag)item);
                }
            }
            else if (Tabelle.Equals("BilderEintrag"))
            {
                List<BilderEintrag> collection;
                collection = database.Query<BilderEintrag>("SELECT * FROM [BilderEintrag]");
                foreach (BilderEintrag item in collection)
                {
                    list.add((IListeneintrag)item);
                }
            }
            else
            {
                return null;
            }

            return list;
        }
    }
}

