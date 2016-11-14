using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik
{
    class Controller
    {
        private Listenklasse<IListeneintrag> menue = new Listenklasse<IListeneintrag>();
        private static Controller controller;
        private DatenbankAdapter dba;
        
        private Controller(Listenklasse<IListeneintrag> menue)
        {
            this.dba = DatenbankAdapter.getInstance();
            menue = this.dba.getObject()
        }
    }
}
