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
    public partial class Listenverzeichnis : ContentPage, IElementverwaltung
    {
        private IListeneintrag aktuellesElement;

        public IListeneintrag AktuellesElement
        {
            get
            {
                return this.aktuellesElement;
            }

            set { }
        }

        public Listenverzeichnis(string verzeichnis)
        {
            LblTitle.Text = verzeichnis;
            DatenbankAdapter dba = DatenbankAdapter.getInstance();

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
                    Verzeichnis.addRange(dba.select(new Stellplatz().GetType(), "").sortiereEintraegeNachAttribut("distance").getListe());
                    break;
            }
            ListAdapter.ItemsSource = Verzeichnis.getListe();
            InitializeComponent();
        }

        // Todo: Listenverzeichnis für alle Listen deklarieren. 
        // Tagebuchverzeichnis, Stellplätze und Checklisten, sodass auch die jeweiligen Einträge verwaltet werden können

        public Listenverzeichnis(Listenklasse<IListeneintrag> liste)
        {
            this.AktuellesElement = liste;

            // Baue die Liste individuell nach Eintragsart auf.
            // Bsp.: Tagebücher enthalten Tagebucheinträge, welche anders aussehen als Checklisteneinträge
            ListAdapter.ItemsSource = liste.getListe();

            InitializeComponent();
        }

        async void OnItemTapped(object sender, EventArgs e)
        {
            ListView Sender = (ListView)sender;

            IListeneintrag item = (IListeneintrag)Sender.SelectedItem;
            if (item is Listenklasse<TbEintrag>)
                await Navigation.PushAsync(new Listenverzeichnis(((Listenklasse<TbEintrag>)item).getAsGeneral()));
            else if (item is Listenklasse<CLEintrag>)
                await Navigation.PushAsync(new Listenverzeichnis(((Listenklasse<CLEintrag>)item).getAsGeneral()));
            else if (item is Listenklasse<Stellplatz>)
                await Navigation.PushAsync(new Listenverzeichnis(((Listenklasse<Stellplatz>)item).getAsGeneral()));
            else if (item is TbEintrag)
            {
                
            }
            else if (item is CLEintrag)
            {
                ((CLEintrag)item).toggleCheck();
                OnPropertyChanged();
            }
            else if (item is Stellplatz)
                await Navigation.PushAsync(new Stellplatz_Eigenschaften((Stellplatz)item));

        }


    }
}
