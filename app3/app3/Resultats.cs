using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace app3
{   
    [Serializable]
    public class Resultats
    {
        public string matricule;
        public string Matricule
        {
            get { return matricule; }
        }
        public string sigle;
        public string Sigle
        {
            get { return sigle; }
        }
        public string cote;
        public string Cote
        {
            get { return cote; }
        }

        public void setResultats(string matricule, string sigle, string cote)
        {
            this.matricule = matricule;
            this.sigle = sigle;
            this.cote = cote;
        }

        public static bool operator ==(Resultats r1, Resultats r2)
        {
            if (r1.Matricule == r2.Matricule &&
                r1.Sigle == r2.Sigle &&
                r1.Cote == r2.Cote)
            {
                return true;
            }
            else { return false; }
        }

        public static bool operator !=(Resultats r1, Resultats r2)
        {
            if (r1.Matricule != r2.Matricule &&
                r1.Sigle != r2.Sigle &&
                r1.Cote != r2.Cote)
            {
                return true;
            }
            else { return false; }
        }
    }
}
