﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Database
{
    class DB_Standort
    {
        // Mit der Umstellung auf Standort obsolet.

        private double latitude { get; set; }
        private double longtitude { get; set; }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
