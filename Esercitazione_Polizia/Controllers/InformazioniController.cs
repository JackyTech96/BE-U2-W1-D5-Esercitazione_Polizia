using Esercitazione_Polizia.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Esercitazione_Polizia.Controllers
{
    public class InformazioniController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["PoliziaDB"].ConnectionString;

        // GET: MaggioriInfo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VerbaliPerTrasgressore()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            List<Verbale> listaVerbaliPerTrasgressore = new List<Verbale>();

            try
            {
                conn.Open();

                string query = "SELECT T.IDTrasgressore, COUNT(V.IDVerbale) AS TotaleVerbali " +
                           "FROM Trasgressori T " +
                           "LEFT JOIN Verbali V ON T.IDTrasgressore = V.IDTrasgressore " +
                           "GROUP BY T.IDTrasgressore";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Verbale verbale = new Verbale
                    {

                        IDTrasgressore = Convert.ToInt32(reader["IDTrasgressore"]),
                        TotaleVerbali = Convert.ToInt32(reader["TotaleVerbali"])
                    };

                    listaVerbaliPerTrasgressore.Add(verbale);
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Si è verificato un errore: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }

            return View(listaVerbaliPerTrasgressore);
        }
        public ActionResult PuntiDecurtatiPerTrasgressore()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            List<Trasgressore> puntiDecurtatiPerTrasgressore = new List<Trasgressore>();

            try
            {
                conn.Open();

                string query = "SELECT T.IDTrasgressore, SUM(Violazioni.PuntiDecurtati) AS TotalePuntiDecurtati " +
                               "FROM Trasgressori T " +
                               "LEFT JOIN Verbali V ON T.IDTrasgressore = V.IDTrasgressore " +
                               "LEFT JOIN Violazioni ON V.IDViolazione = Violazioni.IDViolazione " +
                               "GROUP BY T.IDTrasgressore";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Trasgressore trasgressore = new Trasgressore
                    {
                        IDTrasgressore = Convert.ToInt32(reader["IDTrasgressore"]),
                        TotalePuntiDecurtati = Convert.ToInt32(reader["TotalePuntiDecurtati"])
                    };

                    puntiDecurtatiPerTrasgressore.Add(trasgressore);
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Si è verificato un errore: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }

            return View(puntiDecurtatiPerTrasgressore);
        }
        public ActionResult ViolazioniOltre10Punti()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            List<Violazione> violazioniOltre10Punti = new List<Violazione>();

            try
            {
                conn.Open();

                string query = "SELECT V.Importo, T.Cognome, T.Nome, VB.DataViolazione, V.PuntiDecurtati " +
                "FROM Violazioni V " +
                "JOIN Verbali VB ON V.IDViolazione = VB.IDViolazione " +
                "JOIN Trasgressori T ON VB.IDTrasgressore = T.IDTrasgressore " +
                "WHERE V.PuntiDecurtati > 10";


                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Violazione violazione = new Violazione
                    {
                        Importo = Convert.ToDecimal(reader["Importo"]),
                        Cognome = reader["Cognome"].ToString(),
                        Nome = reader["Nome"].ToString(),
                        DataViolazione = Convert.ToDateTime(reader["DataViolazione"]),
                        PuntiDecurtati = Convert.ToInt32(reader["PuntiDecurtati"])
                    };

                    violazioniOltre10Punti.Add(violazione);
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Si è verificato un errore: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }

            return View(violazioniOltre10Punti);
        }
        public ActionResult ViolazioniImportoMaggioreDi400()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            List<Violazione> violazioniImportoMaggioreDi400 = new List<Violazione>();

            try
            {
                conn.Open();

                string query = "SELECT * FROM Violazioni WHERE Importo > 400";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Violazione violazione = new Violazione
                    {
                        IDViolazione = Convert.ToInt32(reader["IDViolazione"]),
                        Descrizione = reader["Descrizione"].ToString(),
                        PuntiDecurtati = Convert.ToInt32(reader["PuntiDecurtati"]),
                        Importo = Convert.ToDecimal(reader["Importo"]),
                        Contestabile = Convert.ToBoolean(reader["Contestabile"])
                    };

                    violazioniImportoMaggioreDi400.Add(violazione);
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Si è verificato un errore: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }

            return View(violazioniImportoMaggioreDi400);
        }
    }
}