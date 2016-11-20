using SQLite;
using Xamarin.Forms;

namespace WoMo.Logik.Listeneinträge
{
    [Table("BilderEintrag")]
    public class BilderEintrag : IListeneintrag
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Text { get; set; }
        public int BildId { get; set; }

        [Ignore]
        public ImageSource ImageSource { get; set; }

        [Ignore]
        public Listenklasse<BilderEintrag> Superior { get; set; }
        private int superiorid;
        [Column("Superior")]
        public int SuperiorId { get { return this.superiorid; } set { this.superiorid = value; } }


        public BilderEintrag()
        {

        }

        public BilderEintrag(int bildId, Listenklasse<BilderEintrag> superior)
        {
            this.BildId = bildId;
            this.Superior = superior;

            this.superiorid = superior.Id;
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
        
    }
}
