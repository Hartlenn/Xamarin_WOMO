using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Listeneinträge
{
    class CLEintrag : IListeneintrag
    {
        private int id;
        private bool check = false;

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

        public CLEintrag()
        {
            this.id = DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());
        }

        public void toggleCheck()
        {
            if (check)
            {
                check = false;
            }else
            {
                check = true;
            }
        }

        public bool getCheck()
        {
            return check;
        }

        // Inteface Methoden
        public void aktualisierungenSpeichern()
        {
            this.id = DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());

        }
    }
}
