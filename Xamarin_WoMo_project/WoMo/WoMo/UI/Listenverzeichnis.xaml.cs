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
        private IListeneintrag aktuellesElement;

        private ListView ListAdapter = new ListView();
        private Label LblTitle = new Label();


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
            this.aktuellesElement = liste;

            // Baue die Liste individuell nach Eintragsart auf.
            // Bsp.: Tagebücher enthalten Tagebucheinträge, welche anders aussehen als Checklisteneinträge
            switch (liste.Akzeptiert.ToString().ToLower())
            {
                case ("tbeintrag"):
                    
                    break;
                case ("cleintrag"):

                    break;
                case ("stellplatz"):

                    break;                
            }

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
                // Öffne Editor
            }
            else if (item is CLEintrag)
                ((CLEintrag)item).toggleCheck();
            else if (item is Stellplatz)
                await Navigation.PushAsync(new Stellplatz_Eigenschaften());

        }
    }
}
