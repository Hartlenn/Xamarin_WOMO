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
        string Text { get; set; }
        int Id { get; set; }

        XmlSerializer Serializer { get; }

        // Methoden

        int sortiereNachAttribut(IListeneintrag vergleich, string attribut);
    }
}
