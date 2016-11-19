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
            await Navigation.PushAsync(new Listenverzeichnis("stellplätze"));
        }

        async void OnChecklistClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Listenverzeichnis("checklisten"));
        }

        async void OnTagebuchClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Listenverzeichnis("tagebücher"));
        }

        void OnXMLVerwaltungClick(object sender, EventArgs e)
        {

        }
    }
}
