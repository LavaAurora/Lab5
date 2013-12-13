using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Diagnostics.Contracts;

namespace app2
{
    class DBHelper
    {
        private OleDbConnection connexion;
        private OleDbDataAdapter adapter;
        private DataSet autOuvrDS;
        private DataSet dsEtudiants;

        public DBHelper()
        {
            this.adapter = new OleDbDataAdapter();
            this.autOuvrDS = new DataSet();
            this.dsEtudiants = new DataSet();
            this.connexion = Connexion.Instance.getConnexion();
        }

        public void chargerTables()
        {
            OleDbCommand command;

            Int32 counter = 0;
            String[] tables = { "cours", "etudiants", "resultats" };
            String[] selects = { "SELECT * FROM cours",
                                 "SELECT * FROM etudiants",
                                 "SELECT * FROM resultats"
                               };

            // execution des commandes selets
            foreach (String s in selects)
            {
                command = new OleDbCommand(s, this.connexion);

                // remplir le dataset
                adapter.SelectCommand = command;
                adapter.Fill(autOuvrDS, tables[counter]);
                counter++;
            }

        }

        public void chargerEtudiants()
        {
            String select;

            select = "SELECT e.prenom AS Prenom, e.nom AS Nom, c.sigle AS Sigle, c.nom AS Cours ";
            select += "FROM ((etudiants e INNER JOIN resultats r ON e.matricule = r.matricule) ";
            select += "INNER JOIN cours c ON r.sigle = c.sigle)";

            OleDbCommand command = new OleDbCommand(select, this.connexion);

            adapter.SelectCommand = command;
            adapter.Fill(dsEtudiants);

        }

        public void afficherDataset()
        {
            foreach (DataTable table in autOuvrDS.Tables)
            {
                Console.WriteLine("Table : " + table);

                foreach (DataColumn dc in table.Columns)
                    Console.WriteLine(dc.ColumnName + " (" + dc.DataType.Name + ")");

                Console.WriteLine();
            }

            Console.WriteLine("\n");
        }

        public void afficherEtudiants()
        {
            foreach (DataTable table in dsEtudiants.Tables)
            {
                Console.WriteLine("Prenom \t\t Nom \t\t Sigle \t\t Cours");
                Console.WriteLine("================================================================");
                foreach (DataRow dr in table.Rows)
                {

                    Console.WriteLine(dr[0] + "\t\t" + dr[1] + "\t\t" + dr[2] + "\t\t" + dr[3]);
                }
            }

            Console.WriteLine("\n");
        }

        public void afficherRelation()
        {

            // creer la relation etudiants - resultats
            DataColumn pColumn = autOuvrDS.Tables["etudiants"].Columns["matricule"];
            DataColumn cColumn = autOuvrDS.Tables["resultats"].Columns["matricule"];
            DataRelation relEtuRes = new DataRelation("EtudiantsResultats", pColumn, cColumn);

            // creer la relation cours - resultats
            DataColumn pColumn2 = autOuvrDS.Tables["cours"].Columns["sigle"];
            DataColumn cColumn2 = autOuvrDS.Tables["resultats"].Columns["sigle"];
            DataRelation relCoursRes = new DataRelation("CoursResultat", pColumn2, cColumn2);

            // ajout des relation au dataset
            autOuvrDS.Relations.Add(relEtuRes);
            autOuvrDS.Relations.Add(relCoursRes);

            Console.WriteLine("Prenom \t\t Nom \t\t Sigle \t\t Cours");
            Console.WriteLine("================================================================");

            foreach (DataRow resultRow in autOuvrDS.Tables["resultats"].Rows)
            {

                foreach (DataRow etudiantRow in resultRow.GetParentRows(relEtuRes))
                {
                    Console.Write(etudiantRow["Prenom"] + "\t\t" + etudiantRow["Nom"] + "\t\t");
                }

                foreach (DataRow coursRow in resultRow.GetParentRows(relCoursRes))
                {
                    Console.WriteLine(coursRow["Sigle"] + "\t\t" + coursRow["Nom"]);
                }

            }

            Console.WriteLine("\n");

        }

        public void afficherDataTable()
        {
            DataTable cours = autOuvrDS.Tables["cours"];
            DataTable etudiants = autOuvrDS.Tables["etudiants"];
            DataTable resultats = autOuvrDS.Tables["resultats"];

            var query =
                from etudiant in etudiants.AsEnumerable()
                join resultat in resultats.AsEnumerable()
                on etudiant.Field<string>("matricule") equals resultat.Field<string>("matricule")
                join cour in cours.AsEnumerable()
                on resultat.Field<string>("sigle") equals cour.Field<string>("sigle")
                select new { etudiant, cour };


            // Définir la structure de la table de sortie
            DataTable table = new DataTable();
            table.Columns.Add("Prenom", typeof(string));
            table.Columns.Add("Nom", typeof(string));
            table.Columns.Add("Sigle", typeof(string));
            table.Columns.Add("Cours", typeof(string));

            Console.WriteLine("Prenom \t\t Nom \t\t Sigle \t\t Cours");
            Console.WriteLine("================================================================");

            // Remplir la table de sortie
            foreach (var row in query)
            {
                DataRow newrow = table.NewRow();
                newrow["Prenom"] = row.etudiant.Field<string>("Prenom");
                newrow["Nom"] = row.etudiant.Field<string>("Nom");
                newrow["Sigle"] = row.cour.Field<string>("Sigle");
                newrow["Cours"] = row.cour.Field<string>("Nom");
                table.Rows.Add(newrow);
                Console.WriteLine(newrow["Prenom"] + "\t\t" + newrow["Nom"] + "\t\t" + newrow["Sigle"] + "\t\t" + newrow["Cours"]);
            }

            Console.WriteLine("\n");

        }

        public void afficherLambda()
        {
            IEnumerable<DataRow> cours = autOuvrDS.Tables["cours"].AsEnumerable();
            IEnumerable<DataRow> etudiants = autOuvrDS.Tables["etudiants"].AsEnumerable();
            IEnumerable<DataRow> resultats = autOuvrDS.Tables["resultats"].AsEnumerable();

            var query =
                resultats.Join(etudiants, r => r.Field<string>("matricule"), e => e.Field<string>("matricule"), (r, e) => new { etudiant = e, resultat = r })
                .Join(cours, r => r.resultat.Field<string>("sigle"), c => c.Field<string>("sigle"), (r, c) => new { r.etudiant, r.resultat, cour = c })
                .Select(r => new
                {
                    r.cour,
                    r.etudiant,
                    r.resultat
                });

            Console.WriteLine("Prenom \t\t Nom \t\t Sigle \t\t Cours");
            Console.WriteLine("================================================================");

            foreach (var row in query)
            {
                Console.WriteLine(
                                    row.etudiant.Field<string>("Prenom") + "\t\t" +
                                    row.etudiant.Field<string>("Nom") + "\t\t" +
                                    row.cour.Field<string>("Sigle") + "\t\t" +
                                    row.cour.Field<string>("Nom"));
            }

        }


    }
}
