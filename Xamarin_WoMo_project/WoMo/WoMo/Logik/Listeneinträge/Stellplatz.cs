using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Listeneinträge
{
    class Stellplatz : IListeneintrag
    {

        private int id;
        private Standort standort;
        private Listenklasse<CLEintrag> eigenschaftsListe;
        private Listenklasse<BilderEintrag> bilderListe;
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

        internal Standort Standort
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

        internal Listenklasse<CLEintrag> EigenschaftsListe
        {
            get
            {
                return eigenschaftsListe;
            }

            set
            {
                eigenschaftsListe = value;
            }
        }

        internal Listenklasse<BilderEintrag> BilderListe
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

        public Stellplatz(Standort standort, Listenklasse<CLEintrag> eigenschaftsListe, Listenklasse<BilderEintrag> bilderListe)
        {
            this.id = DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());
            this.Standort = standort;
            this.EigenschaftsListe = eigenschaftsListe;
            this.BilderListe = bilderListe;
        }

        public Stellplatz(Standort standort, Listenklasse<BilderEintrag> bilderListe)
        {
            this.id = DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());
            this.Standort = standort;
            this.BilderListe = bilderListe;
            this.EigenschaftsListe = StandardListe;
        }

        public Stellplatz(Standort standort)
        {
            this.id = DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());
            this.Standort = standort;
        }


        // Inteface Methoden
        public void aktualisierungenSpeichern()
        {
            this.id = DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());

        }

    }
}
