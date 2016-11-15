﻿using System;
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
                case ("Datum"):
                    throw new NotImplementedException("Datumssortierung noch nicht verfügbar.");
                default:
                    throw new NotSupportedException("Kenne das Attribut " + attribut + " nicht!");
                
            }
            
        
        }

    }
}