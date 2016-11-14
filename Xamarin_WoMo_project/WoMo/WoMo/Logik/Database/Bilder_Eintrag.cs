using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Database
{
    class Bilder_Eintrag
    {
        [PrimaryKey, AutoIncrement]
        public int BilderEintragID { get; set; }
        public int BilderlisteID { get; set; }
        public String Bildadresse { get; set; }
    }
}
