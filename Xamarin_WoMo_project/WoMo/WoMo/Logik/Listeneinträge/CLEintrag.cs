using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Listeneinträge
{
    class CLEintrag : IListeneintrag
    {
        private int id;
        private string text;
        private bool check = false;
        private Listenklasse<CLEintrag> superior;

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
                return text;
            }

            set
            {
                this.text = value;
                aktualisierungenSpeichern();
            }
        }

        public bool Checked
        {
            get
            {
                return check;
            }

            set
            {
                check = value;
                aktualisierungenSpeichern();
            }
        }

        public CLEintrag()
        {

        }

        public CLEintrag(Listenklasse<CLEintrag> superior)
        {
            this.superior = superior;
            aktualisierungenSpeichern();
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
    }
}
