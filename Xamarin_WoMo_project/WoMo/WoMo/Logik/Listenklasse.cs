using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WoMo.Logik
{
    class Listenklasse<T> : IListeneintrag where T: IListeneintrag
    {
        private List<T> liste = new List<T>();
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
                aktualisierungenSpeichern();
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

        public Type Akzeptiert
        {
            get
            {
                return akzeptiert;
            }

            private set
            {
                akzeptiert = value;
                aktualisierungenSpeichern();
            }
        }

        public Listenklasse(){
            this.id = DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());
        }

        public Listenklasse(string text)
        {
            this.Text = text;
            this.id = DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());
        }

        public Listenklasse(string text, IListeneintrag eintrag)
        {
            this.Text = text;
            this.id = DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());

        }

        public Listenklasse(string text, List<IListeneintrag> liste)
        {
            this.Text = text;
            this.id = DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());

        }

        // Methoden

        /// <summary>
        /// Fügt einen Eintrag des akzeptierten Typs der internen Liste hinzu.
        /// </summary>
        /// <param name="eintrag">Ein Eintrag des Typs IListeneintrag</param>
        public void add(IListeneintrag eintrag)
        {
            if (this.Akzeptiert == null)
            {
                this.Akzeptiert = eintrag.GetType();
            }
            else
            {
                if (eintrag.GetType().Equals(this.Akzeptiert))
                {
                    this.liste.Add((T) eintrag);
                }
                else
                {
                    throw new MyTypeException("Type " + eintrag.GetType().ToString() + " not allowed in this list. Only " + this.Akzeptiert.ToString() + " is accepted.");
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

        public void remove(int index)
        {
            this.liste.RemoveAt(index);
        }

        public void remove(T eintrag)
        {
            this.liste.Remove(eintrag);
        }

        /// <summary>
        /// Sortiert die enthaltenen Listeneinträge nach dem übergebenen Attribut
        /// </summary>
        public void sortiereEintraegeNachAttribut(string attribut)
        {
            for(int i = 0; i < liste.Count; i++)
            {

                for(int j = 0; j<i; j++)
                {
                    if(Controller.sortiereNachAttribut(liste.ElementAt(j), liste.ElementAt(i), attribut) > 0)
                    {
                        T help = liste.ElementAt(j);
                        liste.Insert(j, liste.ElementAt(i));
                        liste.Insert(i, help);
                    }
                }

            }
        }


        // Interface Methoden


        public void aktualisierungenSpeichern()
        {
            this.id = DatenbankAdapter.getInstance().insert(this, this.GetType().ToString());
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


