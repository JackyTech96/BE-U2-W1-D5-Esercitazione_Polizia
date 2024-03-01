using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esercitazione_Polizia.Models
{
    public class Verbale
    {
        public int IDVerbale { get; set; }
        public int IDTrasgressore { get; set; }
        public int IDViolazione { get; set; }
        public DateTime DataViolazione { get; set; }
        public string Nominativo_Agente { get; set; }
        public int TotaleVerbali { get; set; }


        public Verbale() { }

        public Verbale(int iDVerbale, int iDTrasgressore, int iDViolazione, DateTime dataViolazione, string nominativo_Agente)
        {
            IDVerbale = iDVerbale;
            IDTrasgressore = iDTrasgressore;
            IDViolazione = iDViolazione;
            DataViolazione = dataViolazione;
            Nominativo_Agente = nominativo_Agente;
        }
    }
}