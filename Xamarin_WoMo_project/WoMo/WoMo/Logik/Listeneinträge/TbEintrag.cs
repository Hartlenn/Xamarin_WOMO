using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WoMo.Logik.Listeneinträge
{
    class TbEintrag : IListeneintrag
    {
        
        private int id;
        private string text;
        private DateTime datum;
        private Standort standort;
        private Stellplatz stellplatz;

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return id;
            }
            set { }
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

        /// <summary>
        /// Nur wichtig für SQLLite, damit in der Datenbank der Verweis zum Tagebuch richtig stattfindet.
        /// </summary>
        public Listenklasse<TbEintrag> Tagebuch { get; set; }

        public DateTime Datum
        {
            get
            {
                return datum;
            }

            set
            {
                if(value <= DateTime.Now)
                    datum = value;
            }
        }

        /// <summary>
        /// Der Standort an dem der Tagebucheintrag erstellt wurde. 
        /// Schließt einen Stellplatz aus.
        /// </summary>
        public Standort Standort
        {
            get
            {
                return standort;
            }

            set
            {
                stellplatz = null;
                standort = value;
            }
        }

        /// <summary>
        /// Der Stellplatz an dem der Tagebucheintrag erstellt wurde. 
        /// Schließt einen Standort aus, da er bereits im Stellplatz enthalten ist.
        /// </summary>
        public Stellplatz Stellplatz
        {
            get
            {
                return stellplatz;
            }

            set
            {
                standort = null;
                stellplatz = value;
            }
        }

        // Konstruktoren

        public TbEintrag()
        {

        }

        public TbEintrag(Standort standort, DateTime datum, string text, Listenklasse<TbEintrag> tagebuch)
        {
            this.Tagebuch = tagebuch;
            this.Stellplatz = stellplatz;
            this.Datum = datum;
            this.Text = text;
            aktualisierungenSpeichern();
        }

        public TbEintrag(Stellplatz stellplatz, DateTime datum, string text, Listenklasse<TbEintrag> tagebuch)
        {
            this.Tagebuch = tagebuch;
            this.Stellplatz = stellplatz;
            this.Datum = datum;
            this.Text = text;
            aktualisierungenSpeichern();
        }

        public TbEintrag(DateTime datum, string text, Listenklasse<TbEintrag> tagebuch)
        {
            this.Tagebuch = tagebuch;
            this.Datum = datum;
            this.Text = text;
            aktualisierungenSpeichern();
        }

        public TbEintrag(string text, Listenklasse<TbEintrag> tagebuch)
        {
            this.Tagebuch = tagebuch;
            this.Datum = DateTime.Now;
            this.Text = text;
            aktualisierungenSpeichern();
        }

        public TbEintrag(Listenklasse<TbEintrag> tagebuch)
        {
            this.Tagebuch = tagebuch;
            this.Datum = DateTime.Now;
            aktualisierungenSpeichern();
        }

        // Methoden

        /// <summary>
        /// Ändert den Text des Tagebucheintrags
        /// </summary>
        /// <param name="text"></param>
        public void aendereText(string text)
        {
            this.Text = text;
            aktualisierungenSpeichern();

        }



        // Inteface Methoden
        public void aktualisierungenSpeichern()
        {
            this.id = DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());

        }
    }
}
