using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace app1
{
    class Program
    {
        static void Main(string[] args)
        {
            string key;
            string matricule;
            string sigle;
            
            Console.WriteLine("ATTENTION : veuiller dimensionner la largeur de la console et son tampon à environ 200 pour bien voir l'affichage");


            Console.WriteLine("Question #1");
            Console.WriteLine("Appuyer pour voir les étudiants");
            key = System.Console.ReadLine();

            Program.showEtudiants();

            Console.WriteLine("Appuyer pour voir les cours");
            key = System.Console.ReadLine();

            Program.showCours();

            Console.WriteLine("Appuyer pour voir les résultats");
            key = System.Console.ReadLine();

            Program.showResultats();


            Console.WriteLine("Appuyer pour continuer");
            Console.WriteLine("Question #1a (calcul de la moyenne)");
            
            matricule = "";

            while (matricule != "x")
            {
                Console.Write("\n\nEntrer le matricule ('x' pour quitter): ");
                matricule = System.Console.ReadLine();

                if (matricule != "x")
                {
                    Console.Write("\n\nEntrer le sigle de cours : ");

                    sigle = System.Console.ReadLine();

                    Console.Write("\n\nLa moyenne est : " + Program.CalculerMoyenne(matricule, sigle));

                    //Attend une entré pour recommencer
                    sigle = System.Console.ReadLine();

                }
            }

            Console.WriteLine("Appuyer pour teminer");
            key = System.Console.ReadLine();


        }

        //Calcule la moyenne d'un cours pour un étudiant donnée
        private static double CalculerMoyenne(string matricule, string sigle)
        {
            string strNote = "";
            DbConnection dbCon = DbConnection.GetInstance();

            OleDbDataReader reader = dbCon.GetOleDbDataReader("SELECT cote " +
                                                               "FROM resultats " +
                                                               "WHERE sigle = '" + sigle + "'" +
                                                               "AND matricule = '" + matricule + "'");


            while (reader.Read())
            {
                strNote = reader[0].ToString();
            }

            reader.Close();

            return Program.ChangerMoyenneEnCote(strNote);
        }


        private static double ChangerMoyenneEnCote(string note)
        {
            double cote = 0;

            if (note.Length > 0)
            {

                switch (note[0])
                {
                    case 'A': { cote = 4; } break;
                    case 'B': { cote = 3; } break;
                    case 'C': { cote = 2; } break;
                    case 'D': { cote = 1; } break;
                }

                if (note.Length >= 2)
                {
                    if (note[1].Equals('+'))
                    {
                        cote += 0.3;
                    }
                    else
                    {
                        if (note[1].Equals('-'))
                        {
                            if (cote > 0)
                            {
                                cote -= 0.3;
                            }
                        }
                    }
                }
            }

            return cote;

            //source : http://www.crepuq.qc.ca/IMG/pdf/Sysnot_2005.pdf
        }

        private static void showEtudiants()
        {
            DbConnection dbCon = DbConnection.GetInstance();

            OleDbDataReader reader = dbCon.GetOleDbDataReader("SELECT * FROM etudiants");

            string g = String.Format("|{0,-12}|{1,-10}|{2,-7}|{3,-10}|{4,-5}|{5,-7}|{6,-14}|{7,-5}|",
                    reader.GetName(0),
                    reader.GetName(1),
                    reader.GetName(2),
                    reader.GetName(3),
                    reader.GetName(4),
                    reader.GetName(5),
                    reader.GetName(6),
                    "Dep.");

            Console.WriteLine(g);

            while (reader.Read())
            {
                //Console.WriteLine(reader[0].ToString());
                g = String.Format("|{0,-12}|{1,-10}|{2,-7}|{3,-10}|{4,-5}|{5,-7}|{6,-14}|{7,-5}|",
                    reader[0].ToString(),
                    reader[1].ToString(),
                    reader[2].ToString(),
                    reader[3].ToString(),
                    reader[4].ToString(),
                    reader[5].ToString(),
                    reader[6].ToString(),
                    reader[7].ToString());

                Console.WriteLine(g);
            }

            reader.Close();


        }

        private static void showCours()
        {
            DbConnection dbCon = DbConnection.GetInstance();

            OleDbDataReader reader = dbCon.GetOleDbDataReader("SELECT * FROM cours");

            string g = String.Format("|{0,-20}|{1,-40}|{2,-70}|{3,-10}|",
                    reader.GetName(0),
                    reader.GetName(1),
                    reader.GetName(2),
                    reader.GetName(3));

            Console.WriteLine(g);

            while (reader.Read())
            {
                //Console.WriteLine(reader[0].ToString());
                g = String.Format("|{0,-20}|{1,-40}|{2,-70}|{3,-10}|",
                    reader[0].ToString(),
                    reader[1].ToString(),
                    reader[2].ToString(),
                    reader[3].ToString());

                Console.WriteLine(g);
            }

            reader.Close();


        }

        private static void showResultats()
        {
            DbConnection dbCon = DbConnection.GetInstance();

            OleDbDataReader reader = dbCon.GetOleDbDataReader("SELECT * FROM resultats");

            string g = String.Format("|{0,-20}|{1,-20}|{2,-20}|",
                    reader.GetName(0),
                    reader.GetName(1),
                    reader.GetName(2));

            Console.WriteLine(g);

            while (reader.Read())
            {
                //Console.WriteLine(reader[0].ToString());
                g = String.Format("|{0,-20}|{1,-20}|{2,-20}|",
                    reader[0].ToString(),
                    reader[1].ToString(),
                    reader[2].ToString());

                Console.WriteLine(g);
            }

            reader.Close();
        }
    }
}
