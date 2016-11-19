using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoMo.Logik;
using WoMo.Logik.Database;
using Xamarin.Forms;

namespace WoMo.UI
{
    public partial class NewList : ContentPage
    {
        bool isTb;
        DatenbankAdapter dba;

        public NewList(bool isTb, DatenbankAdapter dba)
        {
            this.dba = dba;
            this.isTb = isTb;
            InitializeComponent();
            if (isTb)
                Title.Text = "Neues Tagebuch erstellen";
            else
                Title.Text = "Neue Checkliste erstellen";
        }


        async void OnBtnOKClick(object sender, EventArgs e)
        {
            if (isTb)
            {
                DB_Tagebuch buch = new DB_Tagebuch();
                buch.Text = Input.Text;
                dba.insert(buch);
            }
            else
            {
                DB_Checkliste list = new DB_Checkliste();
                list.Text = Input.Text;
                dba.insert(list);
            }
            await Navigation.PopAsync();
        }
    }
}
