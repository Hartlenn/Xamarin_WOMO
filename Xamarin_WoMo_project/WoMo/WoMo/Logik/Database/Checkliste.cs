using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Database
{
    class Checkliste
    {
        [PrimaryKey, AutoIncrement]
        public int ChecklistenID { get; set; }
        public String Bezeichner { get; set; }
    }
}
