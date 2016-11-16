using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoMo.UI;
using Xamarin.Forms;

namespace WoMo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnStellplatzClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Stellplatz_Uebersicht());
        }

        async void OnChecklistClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Checklist());
        }

        async void OnTagebuchClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Tagebuch());
        }

        async void OnXMLVerwaltungClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
