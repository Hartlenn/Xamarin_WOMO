using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Database
{
    class DB_Tagebuch
    {
        public String Bezeichner { get; set; }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
