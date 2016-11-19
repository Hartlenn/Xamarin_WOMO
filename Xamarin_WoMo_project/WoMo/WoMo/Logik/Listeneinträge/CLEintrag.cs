using System;
using SQLite;
using Xamarin.Forms;

namespace WoMo.Logik.Listeneinträge
{
    [Table("CLEintrag")]
    public class CLEintrag : IListeneintrag
    {
        private int id;
        private string text;
        private bool check;

        [Ignore]
        public Listenklasse<CLEintrag> superior { get; set; }
        private int superiorid;
        [Column("superior")]
        public int SuperiorId { get { return superiorid; } set { superiorid = value; } }

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                id = value;
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
        public bool Checked
        {
            get
            {
                return this.check;
            }

            set
            {
                check = value;
            }
        }

        public CLEintrag()
        {

        }

        public CLEintrag(Listenklasse<CLEintrag> superior)
        {
            this.superior = superior;
            this.superiorid = superior.Id;
        }

        public void toggleCheck()
        {
            if (Checked)
            {
                Checked = false;
            }else
            {
                Checked = true;
            }

        }

        // Interface Methoden
        public void aktualisierungenSpeichern()
        {
            this.id = DatenbankAdapter.getInstance().insert(this);
        }

        public string toXml()
        {
            return "<CLEintrag></Id>"
                + Id + "</Id><text>"
                + Text + "</text><checked>"
                + Checked + "</checked><Superior>"
                + superior + "</Superior></CLEintrag>";
        }

        public ViewCell getListViewEintrag()
        {
            throw new NotImplementedException();
        }
    }
}
