using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoMo.Logik;
using WoMo.Logik.Database;
using WoMo.Logik.Listeneinträge;
using Xamarin.Forms;

namespace WoMo.UI
{
    public partial class Tagebuch : ContentPage
    {
        DatenbankAdapter dba;
        TbEintrag eintrag;
        bool isEdit;
        int superiorid;
        public Tagebuch(DatenbankAdapter dba, int superiorid)
        {
            this.eintrag = new TbEintrag();
            InitializeComponent();
            EntryDate.Date = DateTime.Today;
            this.dba = dba;
            isEdit = false;
            this.superiorid = superiorid;
        }

        public Tagebuch(TbEintrag eintrag, DatenbankAdapter dba, int superiorid)
        {
            this.eintrag = eintrag;
            InitializeComponent();
            Entry.Text = eintrag.Text;
            EntryDate.Date = eintrag.Datum;
            this.dba = dba;
            this.superiorid = superiorid;
            isEdit = true;
        }
        async void OnBtnOKClick(object sender, EventArgs e)
        {
            eintrag.Text = Entry.Text;
            eintrag.Datum = EntryDate.Date;
            eintrag.SuperiorId = superiorid;
            if (isEdit)
                dba.update(eintrag);
            else
                dba.insert(eintrag);
            await Navigation.PopAsync();
        }
    }
}
