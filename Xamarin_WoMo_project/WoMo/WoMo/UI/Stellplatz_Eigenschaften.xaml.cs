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


        public Stellplatz_Eigenschaften(Stellplatz stellplatz)
        {
            this.aktuellesElement = stellplatz;

            TxtBezeichnung.Text = this.aktuellesElement.Text;
            TxtPosition.Text = this.aktuellesElement.Standort.Longitude + ";" + this.aktuellesElement.Standort.Latitude;
            ListAdapter.ItemsSource = this.aktuellesElement.EigenschaftsListe.getListe();
            BildListAdapter.ItemsSource = this.aktuellesElement.BilderListe.getListe();
            
            InitializeComponent();
        }

        public void OnBtnHinzuBildClicked(object sender, EventArgs e)
        {
            // Öffne Bildexplorer des Geräts

            int bildid = 0;
            new BilderEintrag(bildid, aktuellesElement.BilderListe);
            OnPropertyChanged();
        }

        public void OnBtnHinzuEintragClicked(object sender, EventArgs e)
        {
            // Öffne Texteditor zum Schreiben der Checkliste
            string text = "";
            new CLEintrag(aktuellesElement.EigenschaftsListe).Text = text;
            OnPropertyChanged();


        }

    }
}
