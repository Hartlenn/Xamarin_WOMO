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

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(Stellplatz), null);
        public static readonly BindableProperty CLTextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(CLEintrag), null);

        public IListeneintrag AktuellesElement
        {
            get
            {
                return aktuellesElement;
            }

            set
            {
                // this.aktuellesElement = value;
            }
        }

        /// <summary>
        /// Konstruktor für neue Elemente
        /// </summary>
        public Stellplatz_Eigenschaften()
        {

        }

        public Stellplatz_Eigenschaften(Stellplatz stellplatz)
        {
            InitializeComponent();
            this.aktuellesElement = stellplatz;

            TxtBezeichnung.Text = this.aktuellesElement.Text;
            TxtPosition.Text = this.aktuellesElement.Standort.Longitude + ";" + this.aktuellesElement.Standort.Latitude;
            try
            {
                ListAdapter.ItemsSource = this.aktuellesElement.EigenschaftsListe.getListe();
                BildListAdapter.ItemsSource = this.aktuellesElement.BilderListe.getListe();
            }
            catch
            {

            }
            
            
        }

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

    }
}
