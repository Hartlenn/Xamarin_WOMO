using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Database
{
    class Bilderliste
    {
        [PrimaryKey, AutoIncrement]
        public int BilderlistenID { get; set; }
        public int StellplatzID { get; set; }

    }
}
