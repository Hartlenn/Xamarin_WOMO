using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Database
{
    class DB_Tagebuch_Eintrag
    {
        public DateTime Datum { get; set; }
        public String Inhalt { get; set; }
        public int TagebuchID { get; set; }


        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
