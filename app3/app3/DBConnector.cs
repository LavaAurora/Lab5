using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace app3
{
    public class DBConnector
    {
        private string chConn;
        private OleDbConnection maConn;
        private OleDbDataAdapter oDA;
        private DataSet oDS;
        private string query;

        public DBConnector()
        {
            chConn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:/cours.mdb";
            maConn = new OleDbConnection(chConn);
        }

        public void connect()
        {
            maConn.Open();
        }

        public void disconnect()
        {
            maConn.Close();
        }

        public void getDataFromCours(Program.Tables table, List<Cours> liste)
        {
            query = "SELECT * FROM " + table.ToString();
            oDA = new OleDbDataAdapter(query, maConn);
            oDS = new DataSet();
            oDA.Fill(oDS, table.ToString());

            foreach (DataRow dr in oDS.Tables[0].Rows)
            {
                Cours coursTemp = new Cours();
                coursTemp.setCours(dr["sigle"].ToString(), dr["nom"].ToString(), dr["description"].ToString(), int.Parse(dr["credit"].ToString()));
                liste.Add(coursTemp);
            }
        }

        public void getDataFromEtudiants(Program.Tables table, List<Etudiants> liste)
        {
            query = "SELECT * FROM " + table.ToString();
            oDA = new OleDbDataAdapter(query, maConn);
            oDS = new DataSet();
            oDA.Fill(oDS, table.ToString());

            foreach (DataRow dr in oDS.Tables[0].Rows)
            {
                Etudiants etudiantsTemp = new Etudiants();
                etudiantsTemp.setEtudiants(dr["matricule"].ToString(), dr["nom"].ToString(), dr["prenom"].ToString(),
                    dr["adresse"].ToString(), dr["ville"].ToString(), dr["code"].ToString(), dr["tel"].ToString(), int.Parse(dr["departement"].ToString()));
                liste.Add(etudiantsTemp);
            }
        }

        public void getDataFromResultats(Program.Tables table, List<Resultats> liste)
        {
            query = "SELECT * FROM " + table.ToString();
            oDA = new OleDbDataAdapter(query, maConn);
            oDS = new DataSet();
            oDA.Fill(oDS, table.ToString());

            foreach (DataRow dr in oDS.Tables[0].Rows)
            {
                Resultats resultatsTemp = new Resultats();
                resultatsTemp.setResultats(dr["matricule"].ToString(), dr["sigle"].ToString(), dr["cote"].ToString());
                liste.Add(resultatsTemp);
            }
        }
    }
}
