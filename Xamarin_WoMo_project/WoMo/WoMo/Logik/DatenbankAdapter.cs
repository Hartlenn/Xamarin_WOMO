using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.IO;
using WoMo.Logik.Listeneinträge;
using WoMo.Logik.Database;
using WoMo.Logik;
using Xamarin.Forms;

namespace WoMo.Logik
{
    class DatenbankAdapter
    {
        static object locker = new object();
        SQLiteConnection database;
        private static DatenbankAdapter singleton;

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
                var connection = DependencyService.Get<SQLite_Adapter>();
                database = connection.GetConnection();
                erstelleObjekte();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void erstelleObjekte()
        {
            //leere();
            database.CreateTable<BilderEintrag>();
            database.CreateTable<CLEintrag>();
            database.CreateTable<Standort>();
            database.CreateTable<Stellplatz>();
            database.CreateTable<TbEintrag>();
            database.CreateTable<DB_Bilderliste>();
            database.CreateTable<DB_Checkliste>();
            database.CreateTable<DB_Tagebuch>();
        }

        private void leere()
        {
            database.DropTable<BilderEintrag>();
            database.DropTable<CLEintrag>();
            database.DropTable<Standort>();
            database.DropTable<Stellplatz>();
            database.DropTable<TbEintrag>();
            database.DropTable<DB_Bilderliste>();
            database.DropTable<DB_Checkliste>();
            database.DropTable<DB_Tagebuch>();
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
                else if (eintrag is DB_Bilderliste)
                {
                    return database.Update((DB_Bilderliste)eintrag) > 0;
                }
                else if (eintrag is DB_Checkliste)
                {
                    return database.Update((DB_Checkliste)eintrag) > 0;
                }
                else if (eintrag is DB_Tagebuch)
                {
                    return database.Update((DB_Tagebuch)eintrag) > 0;
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
                else if (eintrag is DB_Tagebuch)
                {
                    return database.Delete((DB_Tagebuch)eintrag) > 0;
                }
                else if (eintrag is DB_Checkliste)
                {
                    return database.Delete((DB_Checkliste)eintrag) > 0;
                }
                else if (eintrag is DB_Bilderliste)
                {
                    return database.Delete((DB_Bilderliste)eintrag) > 0;
                }
                else
                    return false;
            }
        }

        public Listenklasse<IListeneintrag> select(Type Tabelle, string Bedingung)
        {
            string tabelle = Tabelle.ToString().ToLower();
            if (Bedingung.Contains(";"))
            {
                throw new NotSupportedException("No SQL Injections allowed");
            }

            Listenklasse<IListeneintrag> list = new Listenklasse<IListeneintrag>();
            if (tabelle.Equals("womo.logik.listeneinträge.stellplatz"))
            {
                foreach (Stellplatz platz in database.Query<Stellplatz>("SELECT * FROM [Stellplatz] " + Bedingung))
                {
                    platz.Standort = ((Standort)select(new Standort().GetType(), "WHERE [ID] = " + platz.StandortID.ToString()).getListe().First());
                    platz.EigenschaftsListe.addRange(select(new CLEintrag().GetType(), "WHERE [ID] = " + platz.EigenschaftsListeId.ToString()).getListe());
                    platz.BilderListe.addRange(select(new BilderEintrag().GetType(), "WHERE [ID] = " + platz.BilderListeId.ToString()).getListe());
                    list.add(platz);
                }                 
            }
            else if (tabelle.Equals("womo.logik.listeneinträge.cleintrag"))
            {
                list.addRange(database.Query<CLEintrag>("SELECT * FROM [CLEintrag] " + Bedingung));
            }
            else if (tabelle.Equals("womo.logik.listeneinträge.tbeintrag"))
            {
                foreach(TbEintrag eintrag in database.Query<TbEintrag>("SELECT * FROM [TbEintrag] " + Bedingung))
                {
                    if(eintrag.StandortID != -1)
                    {
                        eintrag.Standort = ((Standort)select(new Standort().GetType(), "WHERE [ID] = " + eintrag.StandortID.ToString()).getListe().First());
                    }
                    else if(eintrag.StellplatzID != -1)
                    {
                        eintrag.Stellplatz = ((Stellplatz)select(new Stellplatz().GetType(), "WHERE [ID] = " + eintrag.StellplatzID.ToString()).getListe().First());
                    }
                    list.add(eintrag);
                }
            }
            else if (tabelle.Equals("womo.logik.listeneinträge.bildereintrag"))
            {
                list.addRange(database.Query<BilderEintrag>("SELECT * FROM [BilderEintrag] " + Bedingung));
            }
            else if (tabelle.Equals("womo.logik.database.db_bilderliste"))
            {
                foreach(DB_List liste in database.Query<DB_Bilderliste>("SELECT * FROM [DB_Bilderliste] " + Bedingung))
                {
                    list.add(DB_List.toListenklasse(new BilderEintrag().GetType(), liste));
                }                
            }
            else if (tabelle.Equals("womo.logik.database.db_checkliste"))
            {
                foreach (DB_List liste in database.Query<DB_Bilderliste>("SELECT * FROM [DB_Checkliste] " + Bedingung))
                {
                    list.add(DB_List.toListenklasse(new CLEintrag().GetType(), liste));
                }
            }
            else if (tabelle.Equals("womo.logik.database.db_tagebuch"))
            {
                foreach (DB_List liste in database.Query<DB_Bilderliste>("SELECT * FROM [DB_Tagebuch] " + Bedingung))
                {
                    list.add(DB_List.toListenklasse(new TbEintrag().GetType(), liste));
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

