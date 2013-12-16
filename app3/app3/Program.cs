using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace app3
{
    public class Program
    {
        public enum Tables { Etudiants, Resultats, Cours };

        static void Main(string[] args)
        {
            List<Etudiants> listeEtudiants = new List<Etudiants>();
            List<Resultats> listeResultats = new List<Resultats>();
            List<Cours> listeCours = new List<Cours>();

            List<Etudiants> listeEtudiants2 = new List<Etudiants>();
            List<Resultats> listeResultats2 = new List<Resultats>();
            List<Cours> listeCours2 = new List<Cours>();

            DBConnector dbConnector = new DBConnector();
            dbConnector.connect();

            dbConnector.getDataFromCours(Tables.Cours, listeCours);
            dbConnector.getDataFromEtudiants(Tables.Etudiants, listeEtudiants);
            dbConnector.getDataFromResultats(Tables.Resultats, listeResultats);

            /*** ETUDIANTS ***/
            var etudiantQuery =
                from etudiant in listeEtudiants
                select etudiant;

            foreach (Etudiants etudiant in etudiantQuery)
            {
                Console.WriteLine("=== Etudiant ===");
                Console.WriteLine("Matricule : " + etudiant.Matricule);
                Console.WriteLine("Nom : " + etudiant.Nom);
                Console.WriteLine("Prenom : " + etudiant.Prenom);
                Console.WriteLine("Adresse : " + etudiant.Adresse);
                Console.WriteLine("Ville : " + etudiant.Ville);
                Console.WriteLine("Code Postal : " + etudiant.Code);
                Console.WriteLine("Telephone : " + etudiant.Tel);
                Console.WriteLine("Departement : " + etudiant.Departement.ToString());
                Console.WriteLine(" ");
            }

            /*** COURS ***/
            var coursQuery =
                from cours in listeCours
                select cours;

            foreach (Cours cours in coursQuery)
            {
                Console.WriteLine("=== Cours ===");
                Console.WriteLine("Sigle : " + cours.Sigle);
                Console.WriteLine("Nom : " + cours.Nom);
                Console.WriteLine("Description : " + cours.Description);
                Console.WriteLine("Credit : " + cours.Credit.ToString());
                Console.WriteLine(" ");
            }

            /*** RESULTATs ***/
            var resultatsQuery =
                from resultat in listeResultats
                select resultat;

            foreach (Resultats resultat in resultatsQuery)
            {
                Console.WriteLine("=== Resultats ===");
                Console.WriteLine("Matricule : " + resultat.Matricule);
                Console.WriteLine("Sigle : " + resultat.Sigle);
                Console.WriteLine("Cote : " + resultat.Cote);
                Console.WriteLine(" ");
            }

            XmlSerializer serialiseurEtudiants = new XmlSerializer(typeof(List<Etudiants>));
            StreamWriter fluxEtudiants = new StreamWriter("etudiants.xml");

            serialiseurEtudiants.Serialize(fluxEtudiants, listeEtudiants);

            fluxEtudiants.Close();

            
            XmlSerializer serialiseurResultats = new XmlSerializer(typeof(List<Resultats>));
            StreamWriter fluxResultats = new StreamWriter("resultats.xml");

            serialiseurResultats.Serialize(fluxResultats, listeResultats);

            fluxResultats.Close();
            

            XmlSerializer serialiseurCours = new XmlSerializer(typeof(List<Cours>));
            StreamWriter fluxCours = new StreamWriter("cours.xml");

            serialiseurCours.Serialize(fluxCours, listeCours);

            fluxCours.Close();

            FileStream fichierEtudiants = new FileStream("etudiants.xml", FileMode.Open);
            FileStream fichierResultats = new FileStream("resultats.xml", FileMode.Open);
            FileStream fichierCours = new FileStream("cours.xml", FileMode.Open);

            // désérialisation
            listeEtudiants2 = (List<Etudiants>)serialiseurEtudiants.Deserialize(fichierEtudiants);
            listeResultats2 = (List<Resultats>)serialiseurResultats.Deserialize(fichierResultats);
            listeCours2 = (List<Cours>)serialiseurCours.Deserialize(fichierCours);

            fichierEtudiants.Close();
            fichierResultats.Close();
            fichierCours.Close();

            for (int x = 0; x < listeEtudiants.Count; x++)
            {
                if (listeEtudiants.ElementAt(x) == listeEtudiants2.ElementAt(x))
                {
                    Console.WriteLine("Aucune difference existe entre les deux listes d'etudiants.");
                    Console.WriteLine(" ");
                }
                else
                {
                    Console.WriteLine("Une difference existe entre les deux listes d'etudiants.");
                    Console.WriteLine(" ");
                }
            }

            for (int x = 0; x < listeCours.Count; x++)
            {
                if (listeCours.ElementAt(x) == listeCours2.ElementAt(x))
                {
                    Console.WriteLine("Aucune difference existe entre les deux listes de cours.");
                    Console.WriteLine(" ");
                }
                else
                {
                    Console.WriteLine("Une difference existe entre les deux listes de cours.");
                    Console.WriteLine(" ");
                }
            }

            for (int x = 0; x < listeResultats.Count; x++)
            {
                if (listeResultats.ElementAt(x) == listeResultats2.ElementAt(x))
                {
                    Console.WriteLine("Aucune difference existe entre les deux listes de resultats.");
                    Console.WriteLine(" ");
                }
                else
                {
                    Console.WriteLine("Une difference existe entre les deux listes de resultats.");
                    Console.WriteLine(" ");
                }
            }

            XElement rootEtudiants = XElement.Load("Etudiants.xml");
            XElement rootCours = XElement.Load("Cours.xml");
            XElement rootResultats = XElement.Load("Resultats.xml");

            IEnumerable<XElement> etudiantsNom =
                from el in rootEtudiants.Elements("Etudiants").Elements("nom")
                select el;

            foreach (XElement e in etudiantsNom)
            {
                Console.WriteLine(e);
            }

            IEnumerable<XElement> etudiantsPrenom =
                from el in rootEtudiants.Elements("Etudiants").Elements("prenom")
                select el;

            foreach (XElement e in etudiantsPrenom)
            {
                Console.WriteLine(e);
            }


            IEnumerable<XElement> coursNom =
                from el in rootCours.Elements("Cours").Elements("nom")
                select el;

            foreach (XElement e in coursNom)
            {
                Console.WriteLine(e);
            }

            IEnumerable<XElement> coursSigle =
                from el in rootCours.Elements("Cours").Elements("sigle")
                select el;

            foreach (XElement e in coursSigle)
            {
                Console.WriteLine(e);
            }

            /*var query = from c in customers
                        join o in orders on c.ID equals o.ID
                        select new { c.Name, o.Product };*/

            /*IEnumerable<XElement> address =
                from el in root.Elements("Address")
                where (string)el.Attribute("Type") == "Billing"
                select el;
            foreach (XElement el in address)
                Console.WriteLine(el);
            */

            /*
            // Join with query expression.
            var result = from e in rootEtudiants
                         join r in rootResultats on (t + 1) equals x
                         select t;

            // Display results.
            foreach (var r in result)
            {
                Console.WriteLine(r);
            }
            */

            // Wait for input
            Console.WriteLine("Press any key to quit.");
            Console.ReadKey();
        }
    }
}
