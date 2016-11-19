using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using WoMo.Logik.Listeneinträge;
using Xamarin.Forms;

namespace WoMo.Logik.Database
{
    abstract class DB_List : IListeneintrag
    {
        public abstract int Id { get; set; }
        public abstract string Text { get; set; }

        // Methoden

        public static Listenklasse<IListeneintrag> toListenklasse(Type tabelle, DB_List liste)
        {
            return new Listenklasse<IListeneintrag>(liste.Text, DatenbankAdapter.getInstance().select(tabelle, "WHERE [Superior] = " + liste.Id));

        }

        // Interface Methoden

        public abstract void aktualisierungenSpeichern();

        public ViewCell getListViewEintrag()
        {
            throw new NotImplementedException();
        }

        public abstract string toXml();
    }
}
