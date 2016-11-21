using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WoMo.Logik;
using WoMo.Logik.Database;
using WoMo.Logik.Listeneinträge;
using Xamarin.Forms;

namespace WoMo.UI
{
    public partial class Stellplatz_Eigenschaften : ContentPage, IElementverwaltung
    {
        private Stellplatz aktuellesElement;
        DatenbankAdapter dba;
        bool isEdit;
        Picker pick = new Picker();
        ListView ListAdapter;
        Button AddCLeintrag;
        ObservableCollection<IListeneintrag> list;
        DataTemplate cell;

        /*
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(Stellplatz), null);
        public static readonly BindableProperty CLTextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(CLEintrag), null);
            */
        public IListeneintrag AktuellesElement
        {
            get
            {
                return aktuellesElement;
            }

            set
            {
                 this.aktuellesElement = (Stellplatz)value;
            }
        }

        /// <summary>
        /// Konstruktor für neue Elemente
        /// </summary>
        public Stellplatz_Eigenschaften(DatenbankAdapter dba)
        {
            this.aktuellesElement = new Stellplatz();
            this.dba = dba;
            isEdit = false;

            InitializeComponent();
            BtnDelete.IsEnabled = false;

            Listenklasse<IListeneintrag> checklists = dba.select(typeof(DB_Checkliste), "");
            foreach(IListeneintrag cleintrag in checklists)
            {
                pick.Items.Add(cleintrag.Id + ": " + cleintrag.Text);
            }
            StackLayout.Children.Insert(StackLayout.Children.Count -1 , pick);
        }

        public Stellplatz_Eigenschaften(Stellplatz stellplatz, DatenbankAdapter dba)
        {
            this.aktuellesElement = stellplatz;
            this.dba = dba;
            isEdit = true;

            list = new ObservableCollection<IListeneintrag>();

            InitializeComponent();
            this.aktuellesElement = stellplatz;

            Bezeichnung.Text = this.aktuellesElement.Text;
            Latitude.Text = this.aktuellesElement.Standort.Latitude.ToString();
            Longitude.Text = this.aktuellesElement.Standort.Longitude.ToString();

            AddCLeintrag = new Button();
            AddCLeintrag.Text = "Checklisteneintrag hinzufügen";
            AddCLeintrag.Clicked += OnBtnHinzuEintragClicked;

            StackLayout.Children.Add(AddCLeintrag);

            ListAdapter = new ListView();

            StackLayout.Children.Add(ListAdapter);
  
            try
            {
                //BildListAdapter.ItemsSource = this.aktuellesElement.BilderListe.getListe();
            }
            catch
            {

            }
            
            
        }

        protected override void OnAppearing()
        {
            if (isEdit)
            {
                cell = new DataTemplate(typeof(SwitchCell));
                cell.SetBinding(SwitchCell.TextProperty, "Text");
                cell.SetBinding(SwitchCell.OnProperty, "Checked");
                foreach (CLEintrag eintrag in dba.select(typeof(CLEintrag), "WHERE [Superior] =" + aktuellesElement.EigenschaftsListeId))
                {   
                    list.Add(eintrag);
                }
                ListAdapter.ItemsSource = list;
                ListAdapter.ItemTemplate = cell;
            }
            base.OnAppearing();
        }

        protected override bool OnBackButtonPressed()
        {
            saveChecklistChanges();
            return base.OnBackButtonPressed();
        }
        async void OnBtnSaveClick(object sender, EventArgs e)
        {
            if (!isEdit)
                aktuellesElement.Standort = new Standort();
            try
            {
                aktuellesElement.Standort.Latitude = Double.Parse(Latitude.Text);
            }
            catch { }
            try
            {
                aktuellesElement.Standort.Longitude = Double.Parse(Longitude.Text);
            }
            catch { }
            aktuellesElement.Text = Bezeichnung.Text;

            if (isEdit)
            {
                saveChecklistChanges();
                dba.update(aktuellesElement.Standort);
                dba.update(aktuellesElement);
            }
            else
            {
                aktuellesElement.StandortID = dba.insert(aktuellesElement.Standort);
                aktuellesElement.EigenschaftsListeId = int.Parse(pick.Items[pick.SelectedIndex].Split(':')[0]);
                dba.insert(aktuellesElement);
            }
            await Navigation.PopAsync();
        }


        async void OnBtnDeleteClick(object sender, EventArgs e)
        {
            dba.delete(aktuellesElement);
            await Navigation.PopAsync();
        }


        private void saveChecklistChanges() {
            if (list != null)
            {
                foreach (IListeneintrag item in list)
                {
                    if (item is CLEintrag)
                    {
                        if (!dba.update(((CLEintrag)item)))
                            dba.insert((CLEintrag)item);
                    }
                }
            }
        }
        /*
        public void OnBtnHinzuBildClicked(object sender, EventArgs e)
        {
            // Öffne Bildexplorer des Geräts

            int bildid = 0;
            aktuellesElement.BilderListe.add(new BilderEintrag(bildid, aktuellesElement.BilderListe));
        }
        */

        async void OnBtnHinzuEintragClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewList(typeof(CLEintrag), dba, aktuellesElement.EigenschaftsListeId));

        }
        
    }
}
