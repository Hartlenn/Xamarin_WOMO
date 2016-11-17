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
    class Stellplatz : IListeneintrag
    {

        private int id;
        private string text;
        private Standort standort;
        private Listenklasse<CLEintrag> eigenschaftsListe;
        private Listenklasse<BilderEintrag> bilderListe;
        private Listenklasse<Stellplatz> superior;

        private static Listenklasse<CLEintrag> standardListe;
        

        public static Listenklasse<CLEintrag> StandardListe
        {
            get
            {
                return standardListe;
            }
            set
            {
                
            }
        }

        public static void addToStandardListe(CLEintrag eintrag)
        {
            StandardListe.add(eintrag);
        }

        public static void removeFromStandardListe(int index)
        {
            StandardListe.remove(index);
        }

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
            }
        }

        public Standort Standort
        {
            get
            {
                return standort;
            }

            set
            {
                standort = value;
            }
        }

        public Listenklasse<CLEintrag> EigenschaftsListe
        {
            get
            {
                if(this.eigenschaftsListe == null)
                {
                    this.eigenschaftsListe = StandardListe;
                }
                return eigenschaftsListe;
            }

            set
            {
                eigenschaftsListe = value;
            }
        }

        public Listenklasse<BilderEintrag> BilderListe
        {
            get
            {
                return bilderListe;
            }

            set
            {
                bilderListe = value;
            }
        }

        public Stellplatz()
        {

        }

        public Stellplatz(Standort standort, Listenklasse<CLEintrag> eigenschaftsListe, Listenklasse<BilderEintrag> bilderListe, Listenklasse<Stellplatz> superior)
        {
            this.Standort = standort;
            this.EigenschaftsListe = eigenschaftsListe;
            this.BilderListe = bilderListe;
            this.superior = superior;
        }

        public Stellplatz(Standort standort, Listenklasse<BilderEintrag> bilderListe, Listenklasse<Stellplatz> superior)
        {
            this.Standort = standort;
            this.BilderListe = bilderListe;
            this.superior = superior;
        }

        public Stellplatz(Standort standort, Listenklasse<Stellplatz> superior)
        {
            this.Standort = standort;
            this.superior = superior;
        }


        // Interface Methoden
        public void aktualisierungenSpeichern()
        {
            this.id = DatenbankAdapter.getInstance().insert(this);

        }

        public string toXml()
        {
            return "<Stellplatz><Id>"
                + Id + "</Id><text>"
                + Text + "</text><Standort>"
                + Standort.Id + "</Standort><eigenschaftsListe>"
                + EigenschaftsListe.Id + "</eigenschaftsListe><bilderListe>"
                + bilderListe.Id + "</bilderListe><Superior>"
                + superior + "</Superior></Stellplatz>";
        }
        
    }
}
