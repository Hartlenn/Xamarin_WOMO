﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WoMo.Logik;
using WoMo.Logik.Listeneinträge;
using WoMo.Logik.Database;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace WoMo.UI
{
    public partial class Listenverzeichnis : ContentPage, IElementverwaltung
    {
        private IListeneintrag aktuellesElement;
        DatenbankAdapter dba;
        ObservableCollection<IListeneintrag> Celllist;
        private string aktliste;
        private Type contains;
        private IListeneintrag superior;

        public IListeneintrag AktuellesElement
        {
            get
            {
                return this.aktuellesElement;
            }

            set { this.aktuellesElement = value; }
        }

        public Listenverzeichnis(string verzeichnis)
        {
            aktliste = verzeichnis;
            InitializeComponent();

            LblTitle.Text = verzeichnis;
            dba = DatenbankAdapter.getInstance();
        }

        public Listenverzeichnis(Listenklasse<IListeneintrag> liste, IListeneintrag superior, Type contains)
        {
            this.contains = contains;
            this.superior = superior;
            InitializeComponent();
            dba = DatenbankAdapter.getInstance();

            LblTitle.Text = superior.Text;

            this.AktuellesElement = liste;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(aktliste != null)
            {
                Listenklasse<IListeneintrag> Verzeichnis = new Listenklasse<IListeneintrag>(LblTitle.Text);

                String verzeichnis = LblTitle.Text.ToLower();
                switch (verzeichnis)
                {
                    case ("checklisten"):
                        Verzeichnis.addRange(dba.select(new DB_Checkliste().GetType(), "").getListe());
                        break;
                    case ("tagebücher"):
                        Verzeichnis.addRange(dba.select(new DB_Tagebuch().GetType(), "").getListe());
                        break;
                    case ("stellplätze"):
                        Verzeichnis.addRange(dba.select(new Stellplatz().GetType(), "")/*.sortiereEintraegeNachAttribut("distance")*/.getListe());
                        break;
                }
                Celllist = new ObservableCollection<IListeneintrag>();
                DataTemplate template = new DataTemplate(typeof(TextCell));
                template.SetBinding(TextCell.TextProperty, "Text");
                ListAdapter.ItemTemplate = template;

                foreach (IListeneintrag eintrag in Verzeichnis)
                {
                    Celllist.Add(eintrag);
                }

                ListAdapter.ItemsSource = Celllist;

            }
            else if(aktuellesElement != null)
            {
                DataTemplate cell;
                Celllist = new ObservableCollection<IListeneintrag>();

                if (((Listenklasse<IListeneintrag>)aktuellesElement).Akzeptiert == typeof(CLEintrag) || contains == typeof(CLEintrag))
                {
                    cell = new DataTemplate(typeof(SwitchCell));
                    cell.SetBinding(SwitchCell.TextProperty, "Text");
                    cell.SetBinding(SwitchCell.OnProperty, "Checked");
                    //add methode for onChanged
                    foreach (IListeneintrag eintrag in ((Listenklasse<IListeneintrag>)aktuellesElement))
                    {
                        Celllist.Add(eintrag);

                    }
                }
                else if (((Listenklasse<IListeneintrag>)aktuellesElement).Akzeptiert == typeof(TbEintrag) || contains == typeof(TbEintrag))
                {
                    cell = new DataTemplate(typeof(TextCell));
                    cell.SetBinding(TextCell.DetailProperty, "Text");
                    cell.SetBinding(TextCell.TextProperty, "Datum");
                    foreach (IListeneintrag eintrag in ((Listenklasse<IListeneintrag>)aktuellesElement))
                    {
                        Celllist.Add(eintrag);
                    }
                }
                else
                {
                    cell = new DataTemplate(typeof(Label));
                    cell.SetBinding(Label.TextProperty, "Text");
                    foreach (IListeneintrag eintrag in ((Listenklasse<IListeneintrag>)aktuellesElement))
                    {
                        Celllist.Add(eintrag);
                    }
                }

                ListAdapter.ItemTemplate = cell;
                ListAdapter.ItemsSource = Celllist;
            }
            
        }

        async void OnItemTapped(object sender, EventArgs e)
        {
            if (sender is ListView)
            {
                ListView Sender = (ListView)sender;
                IListeneintrag item = (IListeneintrag)Sender.SelectedItem;
                if (aktliste != null)
                {
                    Listenklasse<IListeneintrag> list = new Listenklasse<IListeneintrag>();
                    switch (aktliste)
                    {
                        case ("checklisten"):
                            list = dba.select(new CLEintrag().GetType(), "WHERE [Superior] = " + item.Id);
                            await Navigation.PushAsync(new Listenverzeichnis(list, item, typeof(CLEintrag)));
                            break;
                        case ("tagebücher"):
                            list = dba.select(new TbEintrag().GetType(), "WHERE [Superior] =" + item.Id);
                            await Navigation.PushAsync(new Listenverzeichnis(list, item, typeof(TbEintrag)));
                            break;
                        case ("stellplätze"):
                            await Navigation.PushAsync(new Stellplatz_Eigenschaften((Stellplatz)item, dba));
                            break;
                    }
                }
                else if(item is CLEintrag)
                {
                    CLEintrag eintrag = (CLEintrag)item;
                    if (eintrag.Checked)
                        eintrag.Checked = false;
                    else
                        eintrag.Checked = true;
                }
                else if(item is TbEintrag)
                {
                    await Navigation.PushAsync(new Tagebuch((TbEintrag)item, dba, ((TbEintrag)item).SuperiorId));
                }
            }
        }

        public void switching(object sender, EventArgs e) { }

        async void OnHinzuEintragClick(object sender, EventArgs e)
        {
            if (aktliste != null)
            {
                switch (aktliste)
                {
                    case ("checklisten"):
                        await Navigation.PushAsync(new NewList(false, dba));
                        break;
                    case ("tagebücher"):
                        await Navigation.PushAsync(new NewList(true, dba));
                        break;
                    case ("stellplätze"):
                        await Navigation.PushAsync(new Stellplatz_Eigenschaften(dba));
                        break;
                }
            }
            else if (contains == typeof(CLEintrag))
            {

            }
            else if (contains == typeof(TbEintrag))
            {

                await Navigation.PushAsync(new Tagebuch(dba, superior.Id));
            }
        }

        private void saveChanges()
        {
            if (Celllist != null)
            {
                foreach (IListeneintrag item in Celllist)
                {
                    if (item is CLEintrag)
                    {
                        if (!dba.update(((CLEintrag)item)))
                            dba.insert((CLEintrag)item);
                    }
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            saveChanges();
            return base.OnBackButtonPressed();
        }
    }
}
