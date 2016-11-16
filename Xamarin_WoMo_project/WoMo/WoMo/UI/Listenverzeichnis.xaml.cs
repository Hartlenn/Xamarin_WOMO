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
        private ListView ListAdapter = new ListView();
        private Label Title;


        public Listenverzeichnis(string verzeichnis)
        {
            Title.Text = verzeichnis;
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
                    ((Listenklasse<Stellplatz>)ListAdapter.ItemsSource).;
                    break;
            }
            
            InitializeComponent();
        }

        // Todo: Listenverzeichnis für alle Listen deklarieren. 
        // Tagebuchverzeichnis, Stellplätze und Checklisten, sodass auch die jeweiligen Einträge verwaltet werden können

        public Listenverzeichnis(Type liste)
        {
            InitializeComponent();
        }

        async void OnItemTapped(object sender, EventArgs e)
        {
            IListeneintrag item = (IListeneintrag)((ListView)sender).SelectedItem;
            if (item is Listenklasse<TbEintrag>)
                await Navigation.PushAsync(new Tagebuch());
            else if (item is Listenklasse<CLEintrag>)
                await Navigation.PushAsync(new Checklist());
            else if (item is Listenklasse<Stellplatz>)
                await Navigation.PushAsync(new Stellplatz_Uebersicht());
        }
    }
}
