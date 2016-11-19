using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoMo.Logik.Listeneinträge;
using Xamarin.Forms;

namespace WoMo.UI
{
    public partial class Tagebuch : ContentPage
    {
        public Tagebuch()
        {
            InitializeComponent();
        }

        public Tagebuch(TbEintrag eintrag)
        {

        }
        void OnHinzuEintragClick(object sender, EventArgs e) { }
    }
}
