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

            InitializeComponent();
        }

        
    }
}
