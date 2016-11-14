﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Database
{
    class Tagebuch_Eintrag
    {
        [PrimaryKey, AutoIncrement]
        public int TBEintragID { get; set; }
        public DateTime Datum { get; set; }
        public String Inhalt { get; set; }
        public int TagebuchID { get; set; }
    }
}
