using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace WoMo.Logik.Listeneinträge
{
    [Table("TbEintrag")]
    class TbEintrag : IListeneintrag
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Datum { get; set; }
        [Ignore]
        public Standort Standort { get; set; }
        [Ignore]
        public Stellplatz Stellplatz { get; set; }
        [Ignore]
        public Listenklasse<TbEintrag> superior { get; set; }
        [Ignore]
        public Listenklasse<TbEintrag> Tagebuch { get; set; }

        //add linking only standort or stellplatz allowed
        // Konstruktoren

        public TbEintrag()
        {

        }

        public TbEintrag(Standort standort, DateTime datum, string text, Listenklasse<TbEintrag> tagebuch, Listenklasse<TbEintrag> superior)
        {
            this.Tagebuch = tagebuch;
            this.Standort = standort;
            this.Datum = datum;
            this.Text = text;
            this.superior = superior;

        }

        public TbEintrag(Stellplatz stellplatz, DateTime datum, string text, Listenklasse<TbEintrag> tagebuch, Listenklasse<TbEintrag> superior)
        {
            this.Tagebuch = tagebuch;
            this.Stellplatz = stellplatz;
            this.Datum = datum;
            this.Text = text;
            this.superior = superior;
        }

        public TbEintrag(DateTime datum, string text, Listenklasse<TbEintrag> tagebuch, Listenklasse<TbEintrag> superior)
        {
            this.Tagebuch = tagebuch;
            this.Datum = datum;
            this.Text = text;
            this.superior = superior;
        }

        public TbEintrag(string text, Listenklasse<TbEintrag> tagebuch, Listenklasse<TbEintrag> superior)
        {
            this.Tagebuch = tagebuch;
            this.Datum = DateTime.Now;
            this.Text = text;
            this.superior = superior;
        }

        public TbEintrag(Listenklasse<TbEintrag> tagebuch, Listenklasse<TbEintrag> superior)
        {
            this.Tagebuch = tagebuch;
            this.Datum = DateTime.Now;
            this.superior = superior;
        }

        // Methoden

        /// <summary>
        /// Ändert den Text des Tagebucheintrags
        /// </summary>
        /// <param name="text"></param>
        public void aendereText(string text)
        {
            this.Text = text;

        }



        // Interface Methoden
        public void aktualisierungenSpeichern()
        {
            this.Id = DatenbankAdapter.getInstance().insert(this);

        }

        public string toXml()
        {
            return "<TbEintrag><Id>"
                + Id + "</Id><text>"
                + "</text><Datum>"
                + this.Datum.ToString() + "</Datum><Standort>"
                + Standort.Id + "</Standort><Stellplatz>"
                + Stellplatz.Id + "</Stellplatz><Superior>"
                + superior + "</Superior></TbEintrag>";
        }
        
    }
}
