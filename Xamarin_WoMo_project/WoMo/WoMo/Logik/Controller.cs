﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using WoMo.Logik.FileReadWrite;
using WoMo.Logik.Listeneinträge;
using WoMo.Logik.Database;
using Xamarin.Forms;


namespace WoMo.Logik
{
    /// <summary>
    /// Ein Singleton Controller, um logikweite Funktionen auszuführen. Z.B.: XML Export
    /// </summary>
    public class Controller
    {
        private Listenklasse<IListeneintrag> menue;
        private static Controller controller;
        private DatenbankAdapter dba;
        
        private Controller()
        {
            this.dba = DatenbankAdapter.getInstance();
        }

        // Singleton
        public static Controller getInstance()
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
        public void xmlExportLogik()
        {
            try
            {
                string xml = "<XML><WoMo><menue>"
                    + menue.toXml()
                    + "</menue></WoMo></XML>";

                this.exportFile(xml);
            }catch
            {
            }
        }

        public void xmlExportDatenbank()
        {
            try
            {
                string xml = "<XML><WoMo>";
                DatenbankAdapter dba = DatenbankAdapter.getInstance();

                xml += new Listenklasse<IListeneintrag>("Stellplätze", dba.select(new Stellplatz().GetType(), "")).toXml();

                Listenklasse<IListeneintrag> Verzeichnis = new Listenklasse<IListeneintrag>("Tagebücher");
                foreach (DB_Tagebuch elem in dba.select(new DB_Tagebuch().GetType(), ""))
                {
                    Listenklasse<TbEintrag> tagebuch = new Listenklasse<TbEintrag>(
                        elem.Text, dba.select(new TbEintrag().GetType(), "WHERE [Superior] = " + elem.Id));
                    
                    tagebuch.SuperiorId = elem.Id;
                    Verzeichnis.add(tagebuch);
                }
                xml += Verzeichnis.toXml();

                Verzeichnis = new Listenklasse<IListeneintrag>("Checklisten");
                foreach (DB_Checkliste elem in dba.select(new DB_Checkliste().GetType(), ""))
                {
                    Listenklasse<CLEintrag> checkliste = new Listenklasse<CLEintrag>(
                        elem.Text, dba.select(new CLEintrag().GetType(), "WHERE [Superior] = " + elem.Id));
                    checkliste.SuperiorId = elem.Id;
                    Verzeichnis.add(checkliste);
                }
                xml += Verzeichnis.toXml();

                xml += "</menue></WoMo></XML>";

                this.exportFile(xml);
            }
            catch 
            {
            }

        }

        private async void exportFile(string xml)
        {
            char[] CharArray = xml.ToCharArray();
            byte[] ByteArray = new byte[CharArray.Length];

            for (int i = 0; i < CharArray.Length; i++)
            {
                ByteArray[i] = Convert.ToByte(CharArray[i]);
                
                
            }

            string filename = "WoMo_" + DateTime.Now.ToFileTime() + ".xml";

            try
            {
                
                await DependencyService.Get<IFileReadWrite>()
                    .GetWriteStream(filename)
                    .WriteAsync(ByteArray, 0, ByteArray.Length);
            }
            catch (MyWindowsPhoneFileSystemException mwpfse)
            {
                await DependencyService.Get<IFileWriteForWindows>().WriteFile(filename, xml);
            }
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
                    return elem1.Id.CompareTo(elem2.Id);
                case ("text"):
                    return elem1.Text.CompareTo(elem2.Text);
                case ("datum"):
                    if(elem1 is TbEintrag)
                    {
                        return ((TbEintrag)elem1).Datum.CompareTo(((TbEintrag)elem2).Datum);
                    } else
                    {
                        throw new MyTypeException("Elemente haben kein Datum.");
                    }
                case ("longitude"):
                    if (elem1 is Standort)
                    {
                        return ((Standort)elem1).Longitude.CompareTo(((Standort)elem2).Longitude);
                    }
                    else
                    {
                        throw new MyTypeException("Elemente haben kein Longitude.");
                    }
                case ("latitude"):
                    if (elem1 is Standort)
                    {
                        return ((Standort)elem1).Latitude.CompareTo(((Standort)elem2).Latitude);
                    }
                    else
                    {
                        throw new MyTypeException("Elemente haben kein Longitude.");
                    }
                case ("distanz"):
                    Standort e1, e2;

                    if (elem1 is Standort)
                    {
                        e1 = (Standort)elem1; e2 = (Standort) elem2;
                        
                    }else if(elem1 is Stellplatz)
                    {
                        e1 = ((Stellplatz)elem1).Standort; e2 = ((Stellplatz)elem2).Standort;
                    }
                    else
                    {
                        throw new MyTypeException("Elemente haben keine Koordinaten.");
                    }


                    double longitude, latitude;
                    Standort.getHere(out longitude, out latitude);

                    double distanceFromHereToElem1 = (e1.Longitude - longitude) * (e1.Longitude - longitude)
                        + (e1.Latitude - latitude) * (e1.Latitude - latitude);
                    double distanceFromHereToElem2 = (e2.Longitude - longitude) * (e2.Longitude - longitude)
                        + (e2.Latitude - latitude) * (e2.Latitude - latitude);

                    return distanceFromHereToElem1.CompareTo(distanceFromHereToElem2);

                default:
                    throw new NotSupportedException("Kenne das Attribut " + attribut + " nicht!");

            }
        }

    }
}
