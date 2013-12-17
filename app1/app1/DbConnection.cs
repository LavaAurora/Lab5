using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace app1
{
    public class DbConnection
    {
        private static DbConnection instance;

        //c:/Users/C3PO/Documents/GitHub/Labo5/cours.mdb
        private string dbPath = "../../cours.mdb";      //Chemin de la bd incluant le nom du fichier
        private string username;    //Nom d'utilisateur
        private string password;    //Mot de passe


        private DbConnection()
        {
            
        }

        public static DbConnection GetInstance()
        {

            if (DbConnection.instance == null)
            {
                DbConnection.instance = new DbConnection();
            }

            return DbConnection.instance;
        }

        //Permet de changer tous les infos de la connexion à la bd d'un seul coup
        public void UpdateConnectionInfo(string dbPath, string username, string password)
        {
            this.dbPath = dbPath;
            this.username = username;
            this.password = password;
        }

        //Retourne un reader qui provient d'une requête faite à la bd
        public OleDbDataReader GetOleDbDataReader(string sqlQuery)
        {
            OleDbDataReader reader = null;
            OleDbConnection connection = new OleDbConnection(this.GetConnectionString());
            OleDbCommand command = new OleDbCommand(sqlQuery, connection);

            connection.Open();
            reader = command.ExecuteReader();

            return reader;
        }

        private string GetConnectionString()
        {
            //Si la bd avait un username/password, on ajouterait les infos dans la chaîne
            return @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this.dbPath;
        }
    }
}
