using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Listeneinträge
{
    class BilderEintrag : IListeneintrag
    {
        private int id;
        private string text;
        private int bildId;
        private Listenklasse<BilderEintrag> bilderListe;

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return this.id;
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
                aktualisierungenSpeichern();
            }
        }

        public int BildId
        {
            get
            {
                return bildId;
            }

            set
            {
                bildId = value;
                aktualisierungenSpeichern();
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
                aktualisierungenSpeichern();
            }
        }

        public BilderEintrag()
        {

        }

        public BilderEintrag(int bildId, Listenklasse<BilderEintrag> bilderListe)
        {
            this.BildId = bildId;
            this.BilderListe = bilderListe;
        }

        // Interface Methoden
        public void aktualisierungenSpeichern()
        {
            this.id = DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());

        }
    }
}
