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
            this.superior = eintrag.SuperiorId;
            Input.Text = eintrag.Text;
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
                DB_Tagebuch buch = new DB_Tagebuch();
                buch.Text = Input.Text;
                dba.insert(buch);
            }
            else if (type.Equals(typeof(DB_Checkliste)))
            {
                DB_Checkliste list = new DB_Checkliste();
                list.Text = Input.Text;
                dba.insert(list);
            }
            else if (type.Equals(typeof(CLEintrag)))
            {
                CLEintrag eintrag = new CLEintrag();
                eintrag.SuperiorId = superior;
                eintrag.Text = Input.Text;
                dba.insert(eintrag);
            }
            await Navigation.PopAsync();
        }
        
        async void OnBtnDeleteClick(object sender, EventArgs e)
        {
            dba.delete(aktuellerEintrag);
            await Navigation.PopAsync();
        }
    }
}
