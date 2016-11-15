using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Listeneinträge
{
    class Standort : IListeneintrag
    {
        private int id;
        private string gps;


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

        public string Gps
        {
            get
            {
                return gps;
            }

            set
            {
                gps = value;
            }
        }

        public Standort(string gps)
        {
            this.id = DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());
            this.Gps = gps;
        }

        public void setGpsToHere()
        {
            throw new NotImplementedException();
        }
        




    }
}
