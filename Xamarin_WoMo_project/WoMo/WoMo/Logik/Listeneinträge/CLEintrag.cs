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
        private bool check;

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

        public bool Check
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
            Check = false;
        }

        public void toggleCheck()
        {
            if (Check)
            {
                Check = false;
            }else
            {
                Check = true;
            }

        }

        // Interface Methoden
        public void aktualisierungenSpeichern()
        {
            this.id = DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());
        }
    }
}
