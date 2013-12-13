using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize((Console.WindowWidth * 2), Console.LargestWindowHeight);

            DBHelper helper = new DBHelper();

            helper.chargerTables();
            helper.chargerEtudiants();

            Console.WriteLine("Point A : Afficher les champs et leurs types\n");
            helper.afficherDataset();

            Console.WriteLine("Point B : Afficher les champs et leurs types\n");
            helper.afficherEtudiants();

            Console.WriteLine("Point C : Creer les relations sur le dataset autOuvrDS et afficher\n");
            helper.afficherRelation();

            Console.WriteLine("Point D : LINQ et afficher\n");
            helper.afficherDataTable();

            Console.WriteLine("Point E : Lambda et afficher\n");
            helper.afficherLambda();

            // fermer la connexion db
            Connexion.Instance.close();

            Console.ReadLine();

        }
    }
}
