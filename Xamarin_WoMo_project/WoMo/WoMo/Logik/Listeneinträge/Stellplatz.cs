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
        private int standortid;
        [Column("Standort")]
        public int StandortID { get { return standortid; } set { this.standortid = value; } }

        [Ignore]
        public Listenklasse<CLEintrag> EigenschaftsListe { get; set; }
        private int eigenschaftslisteid;
        [Column("EigenschaftsListe")]
        public int EigenschaftsListeId { get { return eigenschaftslisteid; } set { eigenschaftslisteid = value; } }

        [Ignore]
        public Listenklasse<BilderEintrag> BilderListe { get; set; }
        private int bilderlisteid;
        [Column("BilderListe")]
        public int BilderListeId { get { return bilderlisteid; } set { bilderlisteid = value; } }

        [Ignore]
        public Listenklasse<Stellplatz> Superior { get; set; }
        private int superiorid;
        [Column("Superior")]
        public int SuperiorId { get { return bilderlisteid; } set { superiorid = value; }}

        [Ignore]
        public static Listenklasse<CLEintrag> StandardListe { get; set; }
        private int standardlisteid;
        [Column("StandardListe")]
        public int StandardListeId { get { return bilderlisteid; } set { standardlisteid = value; } }


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
            EigenschaftsListe = new Listenklasse<CLEintrag>();
            BilderListe = new Listenklasse<BilderEintrag>();
        }

        public Stellplatz(Standort standort, Listenklasse<CLEintrag> eigenschaftsListe, Listenklasse<BilderEintrag> bilderListe, Listenklasse<Stellplatz> superior)
        {
            this.Standort = standort;
            this.EigenschaftsListe = eigenschaftsListe;
            this.BilderListe = bilderListe;
            this.Superior = superior;

            eigenschaftslisteid = eigenschaftsListe.Id;
            bilderlisteid = bilderListe.Id;
            standortid = standort.Id;
            superiorid = superior.Id;
        }

        public Stellplatz(Standort standort, Listenklasse<BilderEintrag> bilderListe, Listenklasse<Stellplatz> superior)
        {
            this.Standort = standort;
            this.BilderListe = bilderListe;
            this.Superior = superior;
            
            bilderlisteid = bilderListe.Id;
            standortid = standort.Id;
            superiorid = superior.Id;
        }

        public Stellplatz(Standort standort, Listenklasse<Stellplatz> superior)
        {
            this.Standort = standort;
            this.Superior = superior;

            standortid = standort.Id;
            superiorid = superior.Id;
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
