using SQLite;

namespace WoMo.Logik
{
    interface IListeneintrag
    {
        // Properties
        [PrimaryKey, AutoIncrement]
        int Id { get; set; }


        // Methoden

        int sortiere(IListeneintrag vergleich);
    }
}
