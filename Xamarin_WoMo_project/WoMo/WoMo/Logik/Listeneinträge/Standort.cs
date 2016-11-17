﻿using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Listeneinträge
{
    [Table("Standort")]
    class Standort : IListeneintrag
    {
        private int id;
        private string text;
        private double longitude, latitude;
        private Listenklasse<Standort> superior;

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
        
        public double Longitude {
            get { return longitude; }
            set {
                this.longitude = value;
            }
        }
        public double Latitude {
            get {
                return latitude;
            }
            set {
                this.latitude = value;
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
                text = value;
            }
        }

        public Standort()
        {

        }

        public Standort(Listenklasse<Standort> superior)
        {
            setGpsToHere();
            this.superior = superior;
        }

        public Standort(double longitude, double latitude, Listenklasse<Standort> superior)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.superior = superior;
        }

        public void setGpsToHere()
        {
            throw new NotImplementedException();
        }

        public static void getHere(out double longitude, out double latitude)
        {
            throw new NotImplementedException();
        }

        // Interface Methoden
        public void aktualisierungenSpeichern()
        {
            this.id = DatenbankAdapter.getInstance().insert(this);

        }

        public string toXml()
        {
            return "<Standort><Id>"
                + Id + "</Id><text>"
                + Text + "</text><Longitude>"
                + Longitude + "</Longitude><Latitude>"
                + Latitude + "</Latitude><Superior>"
                + superior + "</Superior></Standort>";
        }
    }
}
