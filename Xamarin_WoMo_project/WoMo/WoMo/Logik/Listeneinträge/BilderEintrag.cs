using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Listeneinträge
{
    class BilderEintrag : IListeneintrag
    {
        private int id;
        private int bildId;

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

        public BilderEintrag(int bildId)
        {
            this.bildId = bildId;
            this.id = DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());
        }

    }
}
