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
            }
        }

        public XmlSerializer Serializer
        {
            get
            {
                return new XmlSerializer(this.GetType());
            }
        }

        public Listenklasse(){
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

        /// <summary>
        /// Sortiert die enthaltenen Listeneinträge nach der ID
        /// </summary>
        public void sortiereEintraegeNachAttribut(string attribut)
        {
            for(int i = 0; i < liste.Count; i++)
            {

                for(int j = 0; j<i; j++)
                {
                    if(liste.ElementAt(j).sortiereNachAttribut(liste.ElementAt(i), attribut) > 0)
                    {
                        T help = liste.ElementAt(j);
                        liste.Insert(j, liste.ElementAt(i));
                        liste.Insert(i, help);
                    }
                }

            }
        }



        // Interface Methoden
            
        public int sortiereNachAttribut(IListeneintrag vergleich, string attribut)
        {
            switch (attribut)
            {
                case("id")
            }
            if (vergleich.GetType().Equals(this.GetType()))
            {
                if(vergleich.Id > this.Id)
                {
                    return -1;
                }else if(vergleich.Id == this.Id)
                {
                    return 0;
                }
                return 1;
            }
            throw new MyTypeException("Can not sort different types. Please adjust your search algorithm.");
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


