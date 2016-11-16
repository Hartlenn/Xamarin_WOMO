using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik
{
    public interface SQLite_Adapter
    {
        SQLiteConnection GetConnection();
    }
}
