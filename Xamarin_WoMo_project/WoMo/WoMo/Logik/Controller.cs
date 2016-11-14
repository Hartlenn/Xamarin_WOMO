using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace WoMo.Logik
{
    /// <summary>
    /// Ein Singleton Controller, um logikweite Funktionen auszuführen. Z.B.: XML Export
    /// </summary>
    class Controller
    {
        private Listenklasse<IListeneintrag> menue;
        private static Controller controller;
        private DatenbankAdapter dba;
        
        private Controller()
        {
            this.dba = DatenbankAdapter.getInstance();
            menue = (Listenklasse<IListeneintrag>) this.dba.getObject("Listenklasse", 0);
        }

        // Singleton
        public Controller getInstance()
        {
            if(controller == null)
            {
                controller = new Controller();
            }

            return controller;
        }


        // Methoden

        public bool xmlExport(Uri pfad)
        {
            throw new NotImplementedException();
        }
        
    }
}
