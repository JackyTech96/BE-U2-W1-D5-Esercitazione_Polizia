using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esercitazione_Polizia.Models
{
    public class Violazione
    {
        public int IDViolazione { get; set; }

        public string Descrizione { get; set; }
        public  int PuntiDecurtati { get; set; }
        public decimal Importo { get; set; }
        public bool Contestabile { get; set; }
        public string Nome {  get; set; }
        public string Cognome { get; set; }
        public DateTime DataViolazione { get; set; }

        public Violazione () { }
        public Violazione(int iDViolazione, string descrizione, int puntiDecurtati, decimal importo, bool contestabile)
        {
            IDViolazione = iDViolazione;
            Descrizione = descrizione;
            PuntiDecurtati = puntiDecurtati;
            Importo = importo;
            Contestabile = contestabile;
        }
    }
}