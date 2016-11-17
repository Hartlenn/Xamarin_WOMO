using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WoMo.Logik;
using WoMo.Logik.Listeneinträge;
using WoMo.Logik.Database;
using Xamarin.Forms;

namespace WoMo.UI
{
    public partial class Listenverzeichnis : ContentPage
    {
        public Listenverzeichnis(string verzeichnis)
        {
            InitializeComponent();
            LblTitle.Text = verzeichnis;
            verzeichnis = verzeichnis.ToLower();
            DatenbankAdapter dba = DatenbankAdapter.getInstance();
            switch (verzeichnis)
            {
                case ("checklisten"):
                    ListAdapter.ItemsSource = dba.select(new DB_Checkliste().GetType(), "").getListe();
                    break;
                case ("tagebücher"):
                    ListAdapter.ItemsSource = dba.select(new DB_Tagebuch().GetType(), "").getListe();
                    break;
                case ("stellplätze"):
                    ListAdapter.ItemsSource = dba.select(new Stellplatz().GetType(), "").sortiereEintraegeNachAttribut("distance").getListe();
                    break;
            }
            
        }

        // Todo: Listenverzeichnis für alle Listen deklarieren. 
        // Tagebuchverzeichnis, Stellplätze und Checklisten, sodass auch die jeweiligen Einträge verwaltet werden können

        public Listenverzeichnis(Listenklasse<IListeneintrag> liste)
        {
            switch (liste.Akzeptiert.ToString().ToLower())
            {
                case (""):
                    break;
            }

            InitializeComponent();
        }

        async void OnItemTapped(object sender, EventArgs e)
        {
            ListView Sender = (ListView)sender;

            IListeneintrag item = (IListeneintrag)Sender.SelectedItem;
            if (item is Listenklasse<TbEintrag>)
                await Navigation.PushAsync(new Listenverzeichnis(((Listenklasse<TbEintrag>) item).getAsGeneral()));
            else if (item is Listenklasse<CLEintrag>)
                await Navigation.PushAsync(new Listenverzeichnis(((Listenklasse<CLEintrag>) item).getAsGeneral()));
            else if (item is Listenklasse<Stellplatz>)
                await Navigation.PushAsync(new Listenverzeichnis(((Listenklasse<Stellplatz>) item).getAsGeneral()));
        }

        void OnHinzuEintragClick(object sender, EventArgs e)
        {
        }
    }
}
