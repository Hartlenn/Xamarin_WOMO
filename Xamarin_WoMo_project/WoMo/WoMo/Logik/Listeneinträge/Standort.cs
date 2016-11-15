using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Listeneinträge
{
    class Standort : IListeneintrag
    {
        private int id;
        private string text;
        private double longitude, latitude;

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
                aktualisierungenSpeichern();
            }
        }
        public double Latitude {
            get {
                return latitude;
            }
            set {
                this.latitude = value;
                aktualisierungenSpeichern();
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
                aktualisierungenSpeichern();
            }
        }

        public Standort()
        {
            setGpsToHere();
        }

        public Standort(double longitude, double latitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public void setGpsToHere()
        {
            throw new NotImplementedException();
        }

        // Interface Methoden
        public void aktualisierungenSpeichern()
        {
            this.id = DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());

        }



    }
}
