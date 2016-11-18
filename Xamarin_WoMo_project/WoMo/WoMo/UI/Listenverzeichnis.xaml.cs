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

namespace WoMo.UI
{
    public partial class Listenverzeichnis : ContentPage, IElementverwaltung
    {
        private IListeneintrag aktuellesElement;

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

            set { }
        }

        public Listenverzeichnis(string verzeichnis)
        {
            InitializeComponent();

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

            ObservableCollection<Element> Celllist = new ObservableCollection<Element>();
            ListAdapter.ItemTemplate = new DataTemplate(typeof(Label));

            foreach (IListeneintrag eintrag in Verzeichnis)
            {
                Label lbl = new Label();
                lbl.BindingContext = eintrag;
                lbl.SetBinding(Label.TextProperty, "Text");
                lbl.Text = eintrag.Text;
                Celllist.Add(lbl);
            }

            ListAdapter.ItemsSource = Celllist;
        }

        // Todo: Listenverzeichnis für alle Listen deklarieren. 
        // Tagebuchverzeichnis, Stellplätze und Checklisten, sodass auch die jeweiligen Einträge verwaltet werden können

        public Listenverzeichnis(Listenklasse<IListeneintrag> liste)
        {
            InitializeComponent();

            this.AktuellesElement = liste;


            // Baue die Liste individuell nach Eintragsart auf.
            // Bsp.: Tagebücher enthalten Tagebucheinträge, welche anders aussehen als Checklisteneinträge


            DataTemplate cell;
            ObservableCollection<Element> Celllist = new ObservableCollection<Element>();

            if (liste.Akzeptiert == typeof(CLEintrag))
            {
                cell = new DataTemplate(typeof(SwitchCell));
                foreach (IListeneintrag eintrag in liste)
                {
                    SwitchCell cel = new SwitchCell();
                    cel.BindingContext = eintrag;
                    cel.SetBinding(SwitchCell.TextProperty, "Text");
                    cel.SetBinding(SwitchCell.OnProperty, "Checked");
                    Celllist.Add(cel);
                }
            }else if(liste.Akzeptiert == typeof(TbEintrag))
            {
                cell = new DataTemplate(typeof(Editor));
                foreach (IListeneintrag eintrag in liste)
                {
                    Editor cel = new Editor();
                    cel.BindingContext = eintrag;
                    cel.SetBinding(Editor.TextProperty, "Text");
                    Celllist.Add(cel);
                }
            }else
            {
                cell = new DataTemplate(typeof(Label));
                foreach (IListeneintrag eintrag in liste)
                {
                    Label cel = new Label();
                    cel.BindingContext = eintrag;
                    cel.SetBinding(Label.TextProperty, "Text");
                    Celllist.Add(cel);
                }
            }

            


            ListAdapter.ItemTemplate = cell;
            ListAdapter.ItemsSource = Celllist;

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
            }
            else if (item is Stellplatz)
                await Navigation.PushAsync(new Stellplatz_Eigenschaften((Stellplatz)item));

        }

        public void OnBtnHinzuEintragClicked(object sender, EventArgs e)
        {
            // Öffne Texteditor zum Schreiben der Checkliste
            

        }

        public void OnSwitchTapped(object sender, EventArgs e)
        {
            ((CLEintrag)sender).toggleCheck();
        }
    }
}
