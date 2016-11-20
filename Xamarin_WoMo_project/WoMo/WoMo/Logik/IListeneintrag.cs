using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace WoMo.Logik
{
    public interface IListeneintrag
    {
        // Properties
        [PrimaryKey, AutoIncrement]
        int Id { get; set; }
        string Text { get; set; }
        int SuperiorId { get; set; }

        // Methoden
        void aktualisierungenSpeichern();
        string toXml();




    }
}
