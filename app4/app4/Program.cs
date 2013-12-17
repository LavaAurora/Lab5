using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace app4
{
    class Program
    {

        static void Main(string[] args)
        {
            DbConnection dbConnection = DbConnection.GetInstance();

            /* Étudiants */
            OleDbDataReader reader = dbConnection.GetOleDbDataReader("SELECT * FROM etudiants");
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            dataTable.TableName = "etudiants";
            dataTable.WriteXml("etudiants.xml");
            dataTable.Reset();

            /* Cours */
            reader = dbConnection.GetOleDbDataReader("SELECT * FROM cours");
            dataTable.Load(reader);
            dataTable.TableName = "cours";
            dataTable.WriteXml("cours.xml");
            dataTable.Reset();

            /* Résultats */
            reader = dbConnection.GetOleDbDataReader("SELECT * FROM resultats");
            dataTable.Load(reader);
            dataTable.TableName = "resultats";
            dataTable.WriteXml("resultats.xml");


            Console.WriteLine("Les fichiers ont été créés!!!");
            Console.ReadKey();
        }
    }
}
