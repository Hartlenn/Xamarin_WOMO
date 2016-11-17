using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WoMo.Logik.Listeneinträge
{
    public class BilderEintrag : IListeneintrag
    {
        private int id;
        private string text;
        private int bildId;
        private Listenklasse<BilderEintrag> superior;

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
            }
        }

        public Listenklasse<BilderEintrag> Superior
        {
            get
            {
                return superior;
            }

            set
            {
                superior = value;
            }
        }

        public BilderEintrag()
        {

        }

        public BilderEintrag(int bildId, Listenklasse<BilderEintrag> superior)
        {
            this.BildId = bildId;
            this.Superior = superior;
        }

        // Interface Methoden
        public void aktualisierungenSpeichern()
        {
            this.id = DatenbankAdapter.getInstance().insert(this);

        }

        public string toXml()
        {
            return "<BilderEintrag><Id>" + Id + "</Id><text>"
                + Text + "</text><bildId>"
                + BildId + "</bildId><Superior>"
                + Superior + "</Superior></BilderEintrag>";
        }

        public ViewCell getListViewEintrag()
        {
            throw new NotImplementedException();
        }
    }
}
