﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Database
{
    class DB_TBEintrag_Standort
    {
        // Mit der Umstellung auf TbEintrag obsolet.

        public int TBEintragID { get; set; }
        public int StandortID { get; set; }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
