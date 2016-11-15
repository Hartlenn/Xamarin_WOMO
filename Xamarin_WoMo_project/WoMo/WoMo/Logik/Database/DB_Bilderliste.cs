using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Database
{
    class DB_Bilderliste
    {
        public int StellplatzID { get; set; }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

    }
}
