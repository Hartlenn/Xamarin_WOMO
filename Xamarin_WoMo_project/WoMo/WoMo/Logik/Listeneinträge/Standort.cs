using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WoMo.Logik.Listeneinträge
{
    [Table("Standort")]
    public class Standort : IListeneintrag
    {
        private int id;
        private string text;
        private double longitude, latitude;

        [Ignore]
        private Listenklasse<Standort> superior { get; set; }
        private int superiorid;
        [Column("superior")]
        public int SuperiorId { get { return superiorid; } set { superiorid = value; } }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public double Longitude
        {
            get { return longitude; }
            set
            {
                this.longitude = value;
            }
        }
        public double Latitude
        {
            get
            {
                return latitude;
            }
            set
            {
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
            this.superiorid = this.superior.Id;
        }

        public Standort(double longitude, double latitude, Listenklasse<Standort> superior)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.superior = superior;
            this.superiorid = this.superior.Id;
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
