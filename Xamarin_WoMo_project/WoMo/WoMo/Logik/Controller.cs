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

        /// <summary>
        /// Gibt die derzeit in der Logik befindlichen und durch das Hauptmenü erreichbare Elemente als XML aus.
        /// </summary>
        /// <param name="pfad"></param>
        /// <returns></returns>
        public bool xmlExportLogik(Uri pfad)
        {
            bool b = false;

            try
            {
                string xml = "<XML><WoMo><menue>"
                    + menue.toXml()
                    + "</menue></WoMo></XML>";
                b = true;
            }catch(Exception e)
            {
                b = false;
            }

            // ToDo: Dateiausgabe

            return b;
        }

        public bool xmlExportDatenbank(Uri pfad)
        {
            bool b = false;
            try
            {
                string xml = "<XML><WoMo>";

                xml += DatenbankAdapter.getInstance().select("DB_Bilderliste").toXml();
                xml += DatenbankAdapter.getInstance().select("DB_Checkliste").toXml();
                xml += DatenbankAdapter.getInstance().select("DB_Tagebuch").toXml();
                xml += "</menue></WoMo></XML>";
                b = true;
            }catch(Exception e)
            {
                b = false;
            }
            // ToDo: Dateiausgabe

            return b;
        }

        /// <summary>
        /// Sortiert zwei Elemente gleicher Klasse vom Typ IListeneintrag nach einem bestimmten Attribut.
        /// </summary>
        /// <param name="elem1"></param>
        /// <param name="elem2"></param>
        /// <param name="attribut"></param>
        /// <returns>elem1 ist größer, gleich oder kleiner als elem2 (1;0;-1)</returns>
        public static int sortiereNachAttribut(IListeneintrag elem1, IListeneintrag elem2, string attribut)
        {
            if (!elem1.GetType().Equals(elem2.GetType()))
                throw new MyTypeException("Sortieren von unterschiedlichen Klassen nicht möglich");

            attribut = attribut.ToLower();
            switch (attribut)
            {
                case ("id"):
                    if (elem1.Id > elem2.Id)
                    {
                        return 1;
                    }
                    else if (elem1.Id == elem2.Id)
                    {
                        return 0;
                    }
                    return -1;
                case ("text"):
                    int int1 = Convert.ToInt32(elem1.Text);
                    int int2 = Convert.ToInt32(elem2.Text);

                    if (int1 >  int2)
                    {
                        return 1;
                    }
                    else if (int1 == int2)
                    {
                        return 0;
                    }
                    return -1;
                case ("datum"):
                    if(elem1 is Listeneinträge.TbEintrag)
                default:
                    throw new NotSupportedException("Kenne das Attribut " + attribut + " nicht!");

            }
        }

    }
}
