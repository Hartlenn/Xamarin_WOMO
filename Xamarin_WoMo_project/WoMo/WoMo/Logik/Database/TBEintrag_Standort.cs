using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Database
{
    class TBEintrag_Standort
    {
        [PrimaryKey, AutoIncrement]
        public int TBEintragID { get; set; }
        [PrimaryKey, AutoIncrement]
        public int StandortID { get; set; }
    }
}
