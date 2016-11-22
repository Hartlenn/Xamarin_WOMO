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
    public class TbEintrag : IListeneintrag
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Datum { get; set; }

        [Ignore]
        public Standort Standort { get; set; }
        private int standortid;
        [Column("Standort")]
        public int StandortID{ get { return standortid;} set { standortid = value; } }

        [Ignore]
        public Stellplatz Stellplatz { get; set; }
        private int stellplatzid;
        [Column("Stellplatz")]
        public int StellplatzID { get { return stellplatzid; } set { stellplatzid = value; } }

        [Ignore]
        public Listenklasse<TbEintrag> superior { get; set; }
        private int superiorid;
        [Column("Superior")]
        public int SuperiorId { get { return superiorid; } set { superiorid = value; } }

        /*
        [Ignore]
        public Listenklasse<TbEintrag> Tagebuch { get; set; }
        private int tagebuchid;
        public int TagebuchId { get { return tagebuchid; } set { tagebuchid = value; } }
        */
        //add linking only standort or stellplatz allowed
        // Konstruktoren

        public TbEintrag()
        {

        }

        public TbEintrag(Standort standort, DateTime datum, string text, /*Listenklasse<TbEintrag> tagebuch,*/ Listenklasse<TbEintrag> superior)
        {
            //this.Tagebuch = tagebuch;
            this.Standort = standort;
            this.Datum = datum;
            this.Text = text;
            this.superior = superior;

            //tagebuchid = this.Tagebuch.Id;
            standortid = this.Standort.Id;
            superiorid = this.superior.Id;
            stellplatzid = 0;

        }

        public TbEintrag(Stellplatz stellplatz, DateTime datum, string text, /*Listenklasse<TbEintrag> tagebuch,*/ Listenklasse<TbEintrag> superior)
        {
            //this.Tagebuch = tagebuch;
            this.Stellplatz = stellplatz;
            this.Datum = datum;
            this.Text = text;
            this.superior = superior;


            //tagebuchid = this.Tagebuch.Id;
            stellplatzid = this.Stellplatz.Id;
            superiorid = this.superior.Id;
            standortid = 0;
        }

        public TbEintrag(DateTime datum, string text,/* Listenklasse<TbEintrag> tagebuch , */Listenklasse<TbEintrag> superior)
        {
            //this.Tagebuch = tagebuch;
            this.Datum = datum;
            this.Text = text;
            this.superior = superior;

            //tagebuchid = this.Tagebuch.Id;
            superiorid = this.superior.Id;
        }

        public TbEintrag(string text, /*Listenklasse<TbEintrag> tagebuch,*/ Listenklasse<TbEintrag> superior)
        {
            //this.Tagebuch = tagebuch;
            this.Datum = DateTime.Now;
            this.Text = text;
            this.superior = superior;

            //tagebuchid = this.Tagebuch.Id;
            superiorid = this.superior.Id;
        }

        public TbEintrag(/*Listenklasse<TbEintrag> tagebuch, */Listenklasse<TbEintrag> superior)
        {
            //this.Tagebuch = tagebuch;
            this.Datum = DateTime.Now;
            this.superior = superior;

            //tagebuchid = this.Tagebuch.Id;
            superiorid = this.superior.Id;
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
                + SuperiorId + "</Superior></TbEintrag>";
        }
        
    }
}
