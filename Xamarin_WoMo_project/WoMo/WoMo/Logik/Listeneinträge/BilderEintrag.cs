using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WoMo.Logik.Listeneinträge
{
    [Table("BilderEintrag")]
    class BilderEintrag : IListeneintrag
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Text { get; set; }
        public int BildId { get; set; }
        [Ignore]
        public Listenklasse<BilderEintrag> Superior { get; set; }

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
            this.Id = DatenbankAdapter.getInstance().insert(this);

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
