using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WoMo.Logik;
using WoMo.Logik.Listeneinträge;
using Xamarin.Forms;

namespace WoMo.UI
{
    public partial class Stellplatz_Eigenschaften : ContentPage, IElementverwaltung
    {
        private Stellplatz aktuellesElement;
        DatenbankAdapter dba;
        bool isEdit;

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
        }

        public Stellplatz_Eigenschaften(Stellplatz stellplatz, DatenbankAdapter dba)
        {
            this.aktuellesElement = stellplatz;
            this.dba = dba;
            isEdit = true;



            InitializeComponent();
            this.aktuellesElement = stellplatz;

            Bezeichnung.Text = this.aktuellesElement.Text;
            Latitude.Text = this.aktuellesElement.Standort.Latitude.ToString();
            Longitude.Text = this.aktuellesElement.Standort.Longitude.ToString();
            try
            {
                ListAdapter.ItemsSource = this.aktuellesElement.EigenschaftsListe.getListe();
                //BildListAdapter.ItemsSource = this.aktuellesElement.BilderListe.getListe();
            }
            catch
            {

            }
            
            
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
                dba.update(aktuellesElement.Standort);
                dba.update(aktuellesElement);
            }
            else
            {
                aktuellesElement.StandortID = dba.insert(aktuellesElement.Standort);
                dba.insert(aktuellesElement);
            }
            await Navigation.PopAsync();
        }


        async void OnBtnDeleteClick(object sender, EventArgs e)
        {
            dba.delete(aktuellesElement);
            await Navigation.PopAsync();
        }
        /*
        public void OnBtnHinzuBildClicked(object sender, EventArgs e)
        {
            // Öffne Bildexplorer des Geräts

            int bildid = 0;
            aktuellesElement.BilderListe.add(new BilderEintrag(bildid, aktuellesElement.BilderListe));
        }

        public void OnBtnHinzuEintragClicked(object sender, EventArgs e)
        {
            // Öffne Texteditor zum Schreiben der Checkliste
            string text = "";
            CLEintrag eintrag = new CLEintrag(aktuellesElement.EigenschaftsListe);
            eintrag.Text = text;
            aktuellesElement.EigenschaftsListe.add(eintrag);


        }
        */

    }
}
