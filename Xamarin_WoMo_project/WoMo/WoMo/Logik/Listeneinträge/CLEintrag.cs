using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WoMo.Logik.Listeneinträge
{
    [Table("CLEintrag")]
    public class CLEintrag : IListeneintrag
    {
        private int id;
        private string text;
        private bool check = false;
        [Ignore]
        public Listenklasse<CLEintrag> superior { get; set; }        

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
