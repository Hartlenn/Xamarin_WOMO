using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Database
{
    class DB_Bilder_Eintrag
    {
        // Durch Umstellung auf bilderEintrag obsolet.

        public int BilderlisteID { get; set; }
        public String Bildadresse { get; set; }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
