using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace app2
{
    public sealed class Connexion
    {
        private static Connexion instance = new Connexion();
        private OleDbConnection connexion = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source = cours.mdb");

        static Connexion() { }

        private Connexion() { }

        public static Connexion Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Connexion();
                }

                return instance;
            }
        }

        public OleDbConnection getConnexion()
        {
            try
            {
                connexion.Open();
            }
            catch (OleDbException e)
            {
                Console.WriteLine("Erreur : " + e.ToString());
                Console.ReadLine();
            }

            return connexion;
        }

        public void close()
        {
            this.connexion.Close();
        }

    }
}
