using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Database
{
    class Checklisten_Eintrag
    {
        [PrimaryKey, AutoIncrement]
        public int CLEintragsID { get; set; }
        public String Bezeichner { get; set; }
        public bool Checked { get; set; }
        public int ChecklistenID { get; set; }
    }
}
