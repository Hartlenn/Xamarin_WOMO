using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using WoMo.Logik.Listeneinträge;

namespace WoMo.Logik.Database
{
    abstract class DB_List : IListeneintrag
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get;

            set;
        }

        public string Text
        {
            get;

            set;
        }

        // Methoden

        public static Listenklasse<IListeneintrag> toListenklasse(Type tabelle, DB_List liste)
        {
            return new Listenklasse<IListeneintrag>(liste.Text, DatenbankAdapter.getInstance().select(tabelle, "WHERE [Superior] = " + liste.Id));

        }

        // Interface Methoden

        public abstract void aktualisierungenSpeichern();

        public abstract string toXml();
    }
}
