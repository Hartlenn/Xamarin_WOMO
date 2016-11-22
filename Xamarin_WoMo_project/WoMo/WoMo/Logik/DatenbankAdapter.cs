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
using System.Diagnostics;

namespace WoMo.Logik
{
    public class DatenbankAdapter
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

        private bool initialisiereDatenbank()
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
                else if (eintrag is Standort)
                {
                    return database.Insert((Standort)eintrag);
                }
                else if (eintrag is DB_Checkliste)
                    return database.Insert((DB_Checkliste)eintrag);
                else if (eintrag is DB_Tagebuch)
                    return database.Insert((DB_Tagebuch)eintrag);
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
                    foreach(TbEintrag child in select(typeof(TbEintrag), "WHERE [Superior] = " + eintrag.Id))
                    {
                        delete(child);
                    }
                    return database.Delete((DB_Tagebuch)eintrag) > 0;
                }
                else if (eintrag is DB_Checkliste)
                {
                    foreach (CLEintrag child in select(typeof(CLEintrag), "WHERE [Superior] = " + eintrag.Id))
                    {
                        delete(child);
                    }
                    return database.Delete((DB_Checkliste)eintrag) > 0;
                }
                else if (eintrag is DB_Bilderliste)
                {
                    foreach (BilderEintrag child in select(typeof(BilderEintrag), "WHERE [Superior] = " + eintrag.Id))
                    {
                        delete(child);
                    }
                    return database.Delete((DB_Bilderliste)eintrag) > 0;
                }
                else
                    return false;
            }
        }

        /// <summary>
        /// Stellplatz does not like eigenschaftslisten
        /// </summary>
        /// <param name="Tabelle"></param>
        /// <param name="Bedingung"></param>
        /// <returns></returns>
        public Listenklasse<IListeneintrag> select(Type Tabelle, string Bedingung)
        {
            string tabelle = Tabelle.ToString().ToLower();
            if (Bedingung.Contains(";"))
            {
                throw new NotSupportedException("No SQL Injections allowed");
            }

            Listenklasse<IListeneintrag> list = new Listenklasse<IListeneintrag>();

            switch(tabelle){
                case("womo.logik.listeneinträge.stellplatz"):
                    try
                    {
                        Listenklasse<IListeneintrag> zwili;
                        foreach (Stellplatz platz in database.Query<Stellplatz>("SELECT * FROM [Stellplatz] " + Bedingung))
                        {
                            platz.Standort = ((Standort)select(new Standort().GetType(), "WHERE [ID] = " + platz.StandortID.ToString()).getListe().First());
                            try
                            {
                                //cleinträge
                                zwili = select(new CLEintrag().GetType(), "WHERE [Superior] = " + platz.EigenschaftsListeId.ToString());
                                if (zwili.getList() != null)
                                    platz.EigenschaftsListe.addRange(zwili.getList());

                                //bilderliste
                                zwili = select(new BilderEintrag().GetType(), "WHERE [Id] = " + platz.BilderListeId.ToString());
                                if (zwili.getListe() != null)
                                    platz.BilderListe.addRange(zwili.getListe());
                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine(e.Message);
                            }
                            finally
                            {
                                list.add(platz);
                            }
                        }
                    }
                    catch { };
                    break;
                case ("womo.logik.listeneinträge.cleintrag"):
                    try
                    {
                        list.addRange(database.Query<CLEintrag>("SELECT * FROM [CLEintrag] " + Bedingung));
                    }
                    catch { };

                    break;
                case ("womo.logik.listeneinträge.tbeintrag"):
                    try
                    { 
                        foreach (TbEintrag eintrag in database.Query<TbEintrag>("SELECT * FROM [TbEintrag] " + Bedingung))
                        {
                            if (eintrag.StandortID != 0)
                            {
                                eintrag.Standort = ((Standort)select(new Standort().GetType(), "WHERE [ID] = " + eintrag.StandortID.ToString()).getListe().First());
                            }
                            else if (eintrag.StellplatzID != 0)
                            {
                                eintrag.Stellplatz = ((Stellplatz)select(new Stellplatz().GetType(), "WHERE [ID] = " + eintrag.StellplatzID.ToString()).getListe().First());
                            }
                            list.add(eintrag);
                            }
                    }
                    catch { };
                    break;
                case ("womo.logik.listeneinträge.bildereintrag"):
                    try
                    {
                        list.addRange(database.Query<BilderEintrag>("SELECT * FROM [BilderEintrag] " + Bedingung));
                    }
                    catch { };

                    break;
                case ("womo.logik.database.db_bilderliste"):
                    try
                    {
                        list.addRange(database.Query<DB_Bilderliste>("SELECT * FROM [DB_Bilderliste]" + Bedingung));
                    }
                    catch { };
                    break;
                case ("womo.logik.database.db_checkliste"):
                    try
                    {
                        list.addRange(database.Query<DB_Checkliste>("SELECT * FROM [DB_Checkliste]" + Bedingung));
                    }
                    catch { };
                    break;
                case ("womo.logik.database.db_tagebuch"):
                    try
                    {
                        list.addRange(database.Query<DB_Tagebuch>("SELECT * FROM [DB_Tagebuch]" + Bedingung));
                    }
                    catch { };
                    break;
                case ("womo.logik.listeneinträge.standort"):
                    try
                    {
                        list.addRange(database.Query<Standort>("SELECT * FROM [Standort]" + Bedingung));
                    }
                    catch { }
                    break;
                default:
                    return null;          
            }

            return list;
        }
    }
}

