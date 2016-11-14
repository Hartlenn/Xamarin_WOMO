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
        public String Bezeichner { get; set; }
        public bool Checked { get; set; }
        public int ChecklistenID { get; set; }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int sortiere(IListeneintrag vergleich)
        {
            if (this.Id < vergleich.Id) return -1;
            if (this.Id == vergleich.Id) return 0;
            else return 1;
        }
    }
}
