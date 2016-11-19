using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WoMo.Logik;
using WoMo.Logik.Listeneinträge;
using WoMo.Logik.Database;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace WoMo.UI
{
    public partial class Listenverzeichnis : ContentPage, IElementverwaltung
    {
        private IListeneintrag aktuellesElement;
        DatenbankAdapter dba;
        ObservableCollection<IListeneintrag> Celllist;
        private string aktliste;
        /*
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(Listenklasse<IListeneintrag>), null);
        public static readonly BindableProperty CheckedProperty =
            BindableProperty.Create("Checked", typeof(bool), typeof(CLEintrag), null);
            */
        public IListeneintrag AktuellesElement
        {
            get
            {
                return this.aktuellesElement;
            }

            set { this.aktuellesElement = value; }
        }

        public Listenverzeichnis(string verzeichnis)
        {
            aktliste = verzeichnis;
            InitializeComponent();

            LblTitle.Text = verzeichnis;
            dba = DatenbankAdapter.getInstance();

            Listenklasse<IListeneintrag> Verzeichnis = new Listenklasse<IListeneintrag>(verzeichnis);

            verzeichnis = verzeichnis.ToLower();
            switch (verzeichnis)
            {
                case ("checklisten"):
                    Verzeichnis.addRange(dba.select(new DB_Checkliste().GetType(), "").getListe());
                    break;
                case ("tagebücher"):
                    Verzeichnis.addRange(dba.select(new DB_Tagebuch().GetType(), "").getListe());
                    break;
                case ("stellplätze"):
                    Verzeichnis.addRange(dba.select(new Stellplatz().GetType(), "")/*.sortiereEintraegeNachAttribut("distance")*/.getListe());
                    break;
            }
            Celllist = new ObservableCollection<IListeneintrag>();
            DataTemplate template = new DataTemplate(typeof(TextCell));
            template.SetBinding(TextCell.TextProperty, "Text");
            ListAdapter.ItemTemplate = template;
            
            foreach (IListeneintrag eintrag in Verzeichnis)
            {
                Celllist.Add(eintrag);
            }

            ListAdapter.ItemsSource = Celllist;
        }

        // Todo: Listenverzeichnis für alle Listen deklarieren. 
        // Tagebuchverzeichnis, Stellplätze und Checklisten, sodass auch die jeweiligen Einträge verwaltet werden können

        public Listenverzeichnis(Listenklasse<IListeneintrag> liste, string header, Type contains)
        {
            InitializeComponent();
            dba = DatenbankAdapter.getInstance();

            LblTitle.Text = header;

            this.AktuellesElement = liste;


            // Baue die Liste individuell nach Eintragsart auf.
            // Bsp.: Tagebücher enthalten Tagebucheinträge, welche anders aussehen als Checklisteneinträge


            DataTemplate cell;
            Celllist = new ObservableCollection<IListeneintrag>();

            if (liste.Akzeptiert == typeof(CLEintrag) || contains == typeof(CLEintrag))
            {
                cell = new DataTemplate(typeof(SwitchCell));
                cell.SetBinding(SwitchCell.TextProperty, "Text");
                cell.SetBinding(SwitchCell.IsEnabledProperty, "Checked");
                //add methode for onChanged
                foreach (IListeneintrag eintrag in liste)
                {
                    Celllist.Add(eintrag);
                    
                }
            }
            else if(liste.Akzeptiert == typeof(TbEintrag) || contains == typeof(TbEintrag))
            {
                cell = new DataTemplate(typeof(Editor));
                cell.SetBinding(Editor.TextProperty, "Text");
                foreach (IListeneintrag eintrag in liste)
                {
                    Celllist.Add(eintrag);
                }
            }else
            {
                cell = new DataTemplate(typeof(Label));
                cell.SetBinding(Label.TextProperty, "Text");
                foreach (IListeneintrag eintrag in liste)
                {
                    Celllist.Add(eintrag);
                }
            }

            
            ListAdapter.ItemTemplate = cell;
            ListAdapter.ItemsSource = Celllist;

        }

        async void OnItemTapped(object sender, EventArgs e)
        {
            if (sender is ListView)
            {
                ListView Sender = (ListView)sender;
                IListeneintrag item = (IListeneintrag)Sender.SelectedItem;
                if (aktliste != null)
                {
                    Listenklasse<IListeneintrag> list = new Listenklasse<IListeneintrag>();
                    switch (aktliste)
                    {
                        case ("checklisten"):
                            list = dba.select(new CLEintrag().GetType(), "WHERE [Superior] = " + item.Id);
                            await Navigation.PushAsync(new Listenverzeichnis(list, item.Text, typeof(Checklist)));
                            break;
                        case ("tagebücher"):
                            list = dba.select(new TbEintrag().GetType(), "WHERE [Superior] =" + item.Id);
                            await Navigation.PushAsync(new Listenverzeichnis(list, item.Text, typeof(TbEintrag)));
                            break;
                        case ("stellplätze"):
                            //nur eigenschaften erstmal
                            await Navigation.PushAsync(new Stellplatz_Eigenschaften((Stellplatz)item));
                            break;
                    }
                }
                else if(item is CLEintrag)
                {
                    CLEintrag eintrag = (CLEintrag)item;
                    if (eintrag.Checked)
                        eintrag.Checked = false;
                    else
                        eintrag.Checked = true;
                }
            }
        }

        public void switching(object sender, EventArgs e) { }

        async void OnHinzuEintragClick(object sender, EventArgs e)
        {
            if (sender is ListView)
            {
                ListView Sender = (ListView)sender;
                IListeneintrag item = (IListeneintrag)Sender.SelectedItem;
                if (aktliste != null)
                {
                    Listenklasse<IListeneintrag> list = new Listenklasse<IListeneintrag>();
                    switch (aktliste)
                    {
                        case ("checklisten"):
                            //erstellen neuer checkliste
                            break;
                        case ("tagebücher"):
                            
                            break;
                        case ("stellplätze"):
                            await Navigation.PushAsync(new Stellplatz_Eigenschaften());
                            break;
                    }
                }
                else if (item is CLEintrag)
                {
                }
                else if (item is Stellplatz)
                {

                }
                else if (item is Tagebuch)
                {

                }
            }
        }

        public void OnImageTapped(object sender, EventArgs e)
        {

        }



        public void OnSwitchTapped(object sender, EventArgs e)
        {
            ((CLEintrag)sender).toggleCheck();
        }

        private void saveChanges()
        {
            if (Celllist != null)
            {
                foreach (IListeneintrag item in Celllist)
                {
                    if (item is CLEintrag)
                    {
                        if (!dba.update(((CLEintrag)item)))
                            dba.insert((CLEintrag)item);
                    }
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            saveChanges();
            return base.OnBackButtonPressed();
        }
    }
}
