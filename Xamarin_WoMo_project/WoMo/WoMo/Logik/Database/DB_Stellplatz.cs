using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Database
{
    class DB_Stellplatz
    {
        public int StandortID { get; set; }
        public int BilderlisteID { get; set; }
        public int ChecklistID { get; set; }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
