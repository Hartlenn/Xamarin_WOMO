﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik
{
    interface IListeneintrag
    {
        // Properties
        string Text { get; set; }
        int Id { get; set; }
        
        // Methoden
        
        void sortiere();
        void xmlExport();
    }
}
