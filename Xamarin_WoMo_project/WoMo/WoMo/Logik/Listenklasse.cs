﻿using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace WoMo.Logik
{
    public class Listenklasse<T> : BindableObject,IListeneintrag where T: IListeneintrag
    {
        private ObservableCollection<T> liste = new ObservableCollection<T>();
        private Type akzeptiert;
        private Listenklasse<Listenklasse<T>> superior;

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

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
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

        private int superiorid = 0;
        public int SuperiorId
        {
            get
            {
                if (superior != null)
                    return this.superior.Id;
                else
                    return this.superiorid;
            }

            set
            {
                this.superiorid = value;
            }
        }

        public Listenklasse(){
            
        }

        public Listenklasse(string text)
        {
            this.Text = text;
        }

        public Listenklasse(string text, IListeneintrag eintrag)
        {
            this.Text = text;
            this.add(eintrag);
        }

        public Listenklasse(string text, List<IListeneintrag> liste)
        {
            this.Text = text;
            this.addRange(liste);
        }

        public Listenklasse(string text, Listenklasse<IListeneintrag> liste)
        {
            this.Text = text;
            this.addRange(liste.liste);
        }

        // Methoden

        public ObservableCollection<T> getListe()
        {
            return this.liste;
        }

        public List<T> getList()
        {
            return new List<T>(this.liste);
        }

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
            if (eintrag.GetType().Equals(this.Akzeptiert))
            {
                this.liste.Add((T)eintrag);
            }
            else
            {
                throw new MyTypeException("Type " + eintrag.GetType().ToString() + " not allowed in this list. Only " + this.Akzeptiert.ToString() + " is accepted.");
            }
        }

        /// <summary>
        /// Fügt eine Liste von Einträgen der internen Liste hinzu. 
        /// Einträge von nicht akzeptierten Typen werden nicht in die Liste übertragen.
        /// </summary>
        /// <param name="collection">Eine beliebige Collection des IListenklassen Typs</param>
        public void addRange(IEnumerable<IListeneintrag> collection)
        {
            foreach(IListeneintrag eintrag in collection)
            {
                try
                {
                    this.add(eintrag);
                } catch (MyTypeException mte) {  Debug.WriteLine(mte.Message); }
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
        public Listenklasse<T> sortiereEintraegeNachAttribut(string attribut)
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
            return this;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.liste.GetEnumerator();
        }

        public Listenklasse<IListeneintrag> getAsGeneral()
        {
            List<IListeneintrag> liste = new List<IListeneintrag>();
            foreach(IListeneintrag eintrag in this)
            {
                liste.Add(eintrag);
            }

            return new Listenklasse<IListeneintrag>(this.Text, liste);
        }

        public int getMaxID()
        {
            int maxID = 0;
            foreach(T eintrag in liste)
            {
                if (eintrag.Id > maxID)
                    maxID = eintrag.Id;
            }
            return maxID;
        }


        // Interface Methoden


        public void aktualisierungenSpeichern()
        {
            this.id = DatenbankAdapter.getInstance().insert(this);
        }

        public string toXml()
        {
            string xml = "<Listenklasse>" + "<Akzeptiert>" + Akzeptiert + "</Akzeptiert><Id>"
                + Id + "</Id><text>" + Text + "</text><Superior>" + SuperiorId + "</Superior>"
                + "<Eintraege>";

            foreach(T eintrag in liste)
            {
                xml += eintrag.toXml();
            }
            xml += "</Eintraege></Listenklasse>";

            return xml;
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


