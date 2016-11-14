using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik
{
    class Listenklasse : IListeneintrag
    {
        private List<IListeneintrag> liste = new List<IListeneintrag>();
        private Type akzeptiert;

        private string text;
        private int id;

        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
            }
        }

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

        public Listenklasse(string text, int id)
        {
            this.Text = text;
            this.Id = id;
        }

        public Listenklasse(string text, int id, IListeneintrag eintrag)
        {
            this.Text = text;
            this.Id = id;

        }

        public Listenklasse(string text, int id, List<IListeneintrag> liste)
        {
            this.Text = text;
            this.Id = id;

        }

        // Methoden

        /// <summary>
        /// Fügt einen Eintrag des akzeptierten Typs der internen Liste hinzu.
        /// </summary>
        /// <param name="eintrag">Ein Eintrag des Typs IListeneintrag</param>
        public void add(IListeneintrag eintrag)
        {
            if (this.akzeptiert == null)
            {
                this.akzeptiert = eintrag.GetType();
            }
            else
            {
                if (eintrag.GetType().Equals(this.akzeptiert))
                {
                    this.liste.Add(eintrag);
                }
                else
                {
                    throw new MyTypeException("Type " + eintrag.GetType().ToString() + " not allowed in this list. Only " + this.akzeptiert.ToString() + " is accepted.");
                }
            }
        }

        /// <summary>
        /// Fügt eine Liste von Einträgen der internen Liste hinzu.
        /// </summary>
        /// <param name="collection">Eine beliebige Collection des IListenklassen Typs</param>
        public void addRange(IEnumerable<IListeneintrag> collection)
        {
            foreach(IListeneintrag eintrag in collection)
            {
                try
                {
                    this.add(eintrag);
                } catch (MyTypeException mte) { }
            }
        }



        // Interface Methoden
            
        public int sortiere(IListeneintrag vergleich)
        {
            throw new NotImplementedException();
        }

        public void xmlExport()
        {
            throw new NotImplementedException();
        }
    }

    class MyTypeException : Exception
    {
        public MyTypeException()
        {
        }

        public MyTypeException(string message) : base(message)
        {
        }

        public MyTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}


