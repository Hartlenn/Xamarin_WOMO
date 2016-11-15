using System;
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

        public DateTime Datum
        {
            get
            {
                return datum;
            }

            set
            {
                datum = value;
            }
        }

        // Konstruktoren

        public TbEintrag(DateTime datum, string text)
        {
            this.Datum = datum;
            this.Text = text;
            this.id = DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());
        }

        public TbEintrag(string text)
        {
            this.Datum = DateTime.Now;
            this.Text = text;
            this.id = DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());
        }

        public TbEintrag()
        {
            this.Datum = DateTime.Now;
            this.id = DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());
        }

        // Methoden

        /// <summary>
        /// Ändert den Text des Tagebucheintrags
        /// </summary>
        /// <param name="text"></param>
        public void aendereText(string text)
        {
            this.Text = text;
            DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());
        }



        // Inteface Methoden
        public void aktualisierungenSpeichern()
        {
            this.id = DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());

        }
    }
}
