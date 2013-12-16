using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace app3
{
    [Serializable]
    public class Cours
    {
        public string sigle;
        public string Sigle
        {
            get { return sigle; }
        }
        public string nom;
        public string Nom
        {
            get { return nom; }
        }
        public string description;
        public string Description
        {
            get { return description; }
        }
        public int credit;
        public int Credit
        {
            get { return credit; }
        }


        public void setCours(string sigle, string nom, string description, int credit)
        {
            this.sigle = sigle;
            this.nom = nom;
            this.description = description;
            this.credit = credit;
        }



        public static bool operator ==(Cours c1, Cours c2)
        {
            if (c1.Sigle == c2.Sigle &&
                c1.Nom == c2.Nom &&
                c1.Description == c2.Description &&
                c1.Credit == c2.Credit)
            {
                return true;
            }
            else { return false; }
        }

        public static bool operator !=(Cours c1, Cours c2)
        {
            if (c1.Sigle != c2.Sigle &&
                c1.Nom != c2.Nom &&
                c1.Description != c2.Description &&
                c1.Credit != c2.Credit)
            {
                return true;
            }
            else { return false; }
        }
    }
}
