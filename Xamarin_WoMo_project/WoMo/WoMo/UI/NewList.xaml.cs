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
    public partial class NewList : ContentPage
    {
        Type type;
        DatenbankAdapter dba;
        int superior;
        IListeneintrag aktuellerEintrag;

        public NewList(Type type, DatenbankAdapter dba)
        {
            InitializeComponent();
            Delete.IsEnabled = false;

            this.dba = dba;
            this.type = type;

            if (this.type.ToString().Equals(typeof(DB_Tagebuch).ToString()))
                Title.Text = "Neues Tagebuch erstellen";
            else if (this.type.ToString().Equals(typeof(DB_Checkliste).ToString()))
                Title.Text = "Neue Checkliste erstellen";
        }

        public NewList(Type type, DatenbankAdapter dba, int superior)
        {
            InitializeComponent();
            Delete.IsEnabled = false;

            this.dba = dba;
            this.type = type;
            this.superior = superior;

            Title.Text = "Füge der Checkliste einen Eintrag hinzu:";
        }

        public NewList(Type type, DatenbankAdapter dba, IListeneintrag eintrag)
        {
            InitializeComponent();
            this.dba = dba;
            this.type = type;
            this.superior = eintrag.Id;
            Input.Text = eintrag.Text;
            this.aktuellerEintrag = eintrag;
            if (this.type.Equals(typeof(CLEintrag)))
                Title.Text = "Bearbeite den Checklisteneintrag:";
            else if (this.type.Equals(typeof(DB_Checkliste)))
                Title.Text = "Bearbeite den Checklistennamen";
            else if (this.type.Equals(typeof(DB_Tagebuch)))
                Title.Text = "Bearbeite den Tagebuchnamen";

        }

        async void OnBtnOKClick(object sender, EventArgs e)
        {
            if (type.Equals(typeof(DB_Tagebuch)))
            {
                if (aktuellerEintrag == null)
                    aktuellerEintrag = new DB_Tagebuch();
                
                aktuellerEintrag.Text = Input.Text;

                if(!dba.update(aktuellerEintrag))
                    dba.insert(aktuellerEintrag);
            }
            else if (type.Equals(typeof(DB_Checkliste)))
            {
                if (aktuellerEintrag == null)
                    aktuellerEintrag = new DB_Checkliste();

                aktuellerEintrag.Text = Input.Text;
                if(!dba.update(aktuellerEintrag))
                    dba.insert(aktuellerEintrag);
            }
            else if (type.Equals(typeof(CLEintrag)))
            {
                if(aktuellerEintrag == null)
                    aktuellerEintrag = new CLEintrag();
                aktuellerEintrag.SuperiorId = superior;
                aktuellerEintrag.Text = Input.Text;
                if(!dba.update(aktuellerEintrag))
                    dba.insert(aktuellerEintrag);
            }
            await Navigation.PopAsync();
        }
        
        async void OnBtnDeleteClick(object sender, EventArgs e)
        {
            dba.delete(aktuellerEintrag);
            await Navigation.PopToRootAsync();
        }
    }
}
