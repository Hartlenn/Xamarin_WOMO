﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace WoMo.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new WoMo.App());
        }

        void OnStellplatzClick(object sender, EventArgs e) { }
        void OnChecklistClick(object sender, EventArgs e) { }
        void OnTagebuchClick(object sender, EventArgs e) { }
        void OnXMLVerwaltungClick(object sender, EventArgs e) { }
    }
}
