using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik.Database
{
    [Table("DB_Checkliste")]
    class DB_Checkliste : DB_List
    {
        public String Bezeichner { get; set; }
        /*
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Text{ get;set; }
        */
        public override void aktualisierungenSpeichern()
        {
            throw new NotImplementedException();
        }
        public override string toXml()
        {
            throw new NotImplementedException();
        }
        
    }
}
