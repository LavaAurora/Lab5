using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace app3
{
    [Serializable]
    public class Etudiants
    {
        public string matricule;
        public string Matricule
        {
            get { return matricule; }
        }
        public string nom;
        public string Nom
        {
            get { return nom; }
        }
        public string prenom;
        public string Prenom
        {
            get { return prenom; }
        }
        public string adresse;
        public string Adresse
        {
            get { return adresse; }
        }
        public string ville;
        public string Ville
        {
            get { return ville; }
        }
        public string code;
        public string Code
        {
            get { return code; }
        }
        public string tel;
        public string Tel
        {
            get { return tel; }
        }
        public int departement;
        public int Departement
        {
            get { return departement; }
        }

        public void setEtudiants(string matricule, string nom, string prenom, string adresse, string ville, string code, string tel, int departement)
        {
            this.matricule = matricule;
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.ville = ville;
            this.code = code;
            this.tel = tel;
            this.departement = departement;
        }

        public static bool operator ==(Etudiants e1, Etudiants e2)
        {
            if( e1.Matricule == e2.Matricule && 
                e1.Nom == e2.Nom &&
                e1.Prenom == e2.Prenom &&
                e1.Adresse == e2.Adresse &&
                e1.Ville == e2.Ville &&
                e1.Code == e2.Code &&
                e1.Tel == e2.Tel &&
                e1.Departement == e2.Departement )
            {
                return true;
            }
            else { return false; }
        }

        public static bool operator !=(Etudiants e1, Etudiants e2)
        {
            if (e1.Matricule != e2.Matricule &&
                e1.Nom != e2.Nom &&
                e1.Prenom != e2.Prenom &&
                e1.Adresse != e2.Adresse &&
                e1.Ville != e2.Ville &&
                e1.Code != e2.Code &&
                e1.Tel != e2.Tel &&
                e1.Departement != e2.Departement)
            {
                return true;
            }
            else { return false; }
        }
    }
}
