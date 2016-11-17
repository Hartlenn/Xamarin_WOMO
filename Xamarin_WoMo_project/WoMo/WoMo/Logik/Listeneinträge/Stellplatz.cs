using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WoMo.Logik.Listeneinträge
{
    [Table("Stellplatz")]
    public class Stellplatz : IListeneintrag
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }
        [Ignore]
        public Standort Standort { get; set; }
        [Ignore]
        public Listenklasse<CLEintrag> EigenschaftsListe { get; set; }
        [Ignore]
        public Listenklasse<BilderEintrag> BilderListe { get; set; }
        [Ignore]
        public Listenklasse<Stellplatz> Superior { get; set; }
        [Ignore]
        public static Listenklasse<CLEintrag> StandardListe { get; set; }


        public static void addToStandardListe(CLEintrag eintrag)
        {
            StandardListe.add(eintrag);
        }

        public static void removeFromStandardListe(int index)
        {
            StandardListe.remove(index);
        }
        

        public Stellplatz()
        {

        }

        public Stellplatz(Standort standort, Listenklasse<CLEintrag> eigenschaftsListe, Listenklasse<BilderEintrag> bilderListe, Listenklasse<Stellplatz> superior)
        {
            this.Standort = standort;
            this.EigenschaftsListe = eigenschaftsListe;
            this.BilderListe = bilderListe;
            this.Superior = superior;
        }

        public Stellplatz(Standort standort, Listenklasse<BilderEintrag> bilderListe, Listenklasse<Stellplatz> superior)
        {
            this.Standort = standort;
            this.BilderListe = bilderListe;
            this.Superior = superior;
        }

        public Stellplatz(Standort standort, Listenklasse<Stellplatz> superior)
        {
            this.Standort = standort;
            this.Superior = superior;
        }


        // Interface Methoden
        public void aktualisierungenSpeichern()
        {
            this.Id = DatenbankAdapter.getInstance().insert(this);

        }

        public string toXml()
        {
            return "<Stellplatz><Id>"
                + Id + "</Id><text>"
                + Text + "</text><Standort>"
                + Standort.Id + "</Standort><eigenschaftsListe>"
                + EigenschaftsListe.Id + "</eigenschaftsListe><bilderListe>"
                + BilderListe.Id + "</bilderListe><Superior>"
                + Superior + "</Superior></Stellplatz>";
        }
        
    }
}
