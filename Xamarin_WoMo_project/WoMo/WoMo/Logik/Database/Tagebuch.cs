using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Database
{
    class Tagebuch
    {
        [PrimaryKey, AutoIncrement]
        public int TagebuchID { get; set; }
        public String Bezeichner { get; set; }
    }
}
