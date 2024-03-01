using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esercitazione_Polizia.Models
{
    public class Trasgressore
    {
        public int IDTrasgressore { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string CF { get; set; }
        public string Citta {get; set;}
        public string Indirizzo { get; set; }
        public int CAP {  get; set; }
        public int TotalePuntiDecurtati { get; set; }

        public Trasgressore() { }

        public Trasgressore(int iDTrasgressore, string nome, string cognome, string cF, string citta, string indirizzo, int cAP)
        {
            IDTrasgressore = iDTrasgressore;
            Nome = nome;
            Cognome = cognome;
            CF = cF;
            Citta = citta;
            Indirizzo = indirizzo;
            CAP = cAP;
        }
    }
}