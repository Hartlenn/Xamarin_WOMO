using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SQLite;

namespace WoMo.Logik
{
    class DatenbankAdapter
    {
        // Attribute

        private static DatenbankAdapter dba;
        private string datenbank, user;
        private System.Security.SecureString passwort;
        private static Uri adresse;

        private Dictionary<Type, string> tabellen = new Dictionary<Type, string>();


        public Dictionary<Type, string> Tabellen
        {
            get
            {
                return tabellen;
            }

            set
            {
                tabellen.Add(value.GetType(), value.GetType().FullName);
            }
        }

        // Konstruktor

        private DatenbankAdapter(string pDatenbank, string pUser, System.Security.SecureString pPasswort, Uri pAdresse)
        {
            datenbank = pDatenbank;
            user = pUser;
            passwort = pPasswort;
            adresse = pAdresse;
        }

        public static DatenbankAdapter getInstance()
        {
            if(dba == null)
            {
                System.Security.SecureString password = new System.Security.SecureString();
                dba = new DatenbankAdapter("deufault", "root", password, new Uri("./database"));
            }

            return dba;
        }

        // Methoden
        public int insert(IListeneintrag eintrag, string type)
        {
            bool b = false;

            SqlConnection connection = this.connect();
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            SqlTransaction transaction;
            // Start a local transaction.
            transaction = connection.BeginTransaction("SampleTransaction");

            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            command.Connection = connection;
            command.Transaction = transaction;

            try
            {
                type = type.ToLower();
                string tabelle = "";
                tabellen.TryGetValue(eintrag.GetType(), out tabelle);
                string spalten = this.ConvertArrayToString(eintrag.GetType().GetCustomAttributes(true));
                string values = this.ConvertArrayToString(eintrag.GetType().GetCustomAttributesData().ToArray());


                command.CommandText = "Insert into" + tabelle + "(" + spalten + ") VALUES (" + values + ");";
                command.ExecuteNonQuery();

                // Attempt to commit the transaction.
                transaction.Commit();
                //Console.WriteLine("Records are written to database.");
                b = true;
            }
            catch (Exception ex)
            {
                b = false;
                //Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                //Console.WriteLine("  Message: {0}", ex.Message);

                // Attempt to roll back the transaction.
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    //Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    //Console.WriteLine("  Message: {0}", ex2.Message);
                }
            }

            return b;
        }

        public IListeneintrag getObject(Type klasse, int id) { throw new NotImplementedException(); }
        public IListeneintrag getObject(Type klasse, string suchText) { throw new NotImplementedException(); }
        public IListeneintrag getObject(string klasse, int id) { throw new NotImplementedException(); }




        private SqlConnection connect()
        {

            return new SqlConnection(adresse.AbsolutePath, new SqlCredential(this.user, this.passwort));
        }


        private string ConvertArrayToString(object[] array)
        {
            //
            // Concatenate all the elements into a StringBuilder.
            //
            StringBuilder builder = new StringBuilder();
            
            foreach (object value in array)
            {
                builder.Append(value.ToString());
                builder.Append(", ");
            }
            return builder.ToString().Trim(", ".ToCharArray());
        }
    }
}
