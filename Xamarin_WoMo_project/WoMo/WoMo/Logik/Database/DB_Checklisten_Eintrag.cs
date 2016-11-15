using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Database
{
    class DB_Checklisten_Eintrag
    {
        // Durch Umstellung auf CLEintrag obsolet.

        public String Bezeichner { get; set; }
        public bool Checked { get; set; }
        public int ChecklistenID { get; set; }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
