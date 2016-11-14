using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Database
{
    class Standort
    {
        [PrimaryKey, AutoIncrement]
        private int standortID { get; set; }
        private double latitude { get; set; }
        private double longtitude { get; set; }

      
    }
}
