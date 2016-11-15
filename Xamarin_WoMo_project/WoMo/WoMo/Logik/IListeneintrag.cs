using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WoMo.Logik
{
    interface IListeneintrag
    {
        // Properties
        [PrimaryKey, AutoIncrement]
        int Id { get; set; }


        // Methoden
        void aktualisierungenSpeichern();
        
    }
}
