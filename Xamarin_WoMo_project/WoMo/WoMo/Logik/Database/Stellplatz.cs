using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Database
{
    class Stellplatz
    {
        [PrimaryKey, AutoIncrement]
        public int StellplatzID { get; set; }
        public int StandortID { get; set; }
        public int BilderlisteID { get; set; }
        public int ChecklistID { get; set; }
    }
}
