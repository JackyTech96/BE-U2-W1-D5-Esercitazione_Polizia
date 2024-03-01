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

    public class ViolazioneController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["PoliziaDB"].ConnectionString;

        // GET: Violazione
        public ActionResult Index()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            List<Violazione> listaViolazioni = new List<Violazione>();

            try
            {
                conn.Open();

                string query = "SELECT * FROM Violazioni WHERE Contestabile = 'true'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Violazione violazione = new Violazione(
                        Convert.ToInt32(reader["IDViolazione"]),
                        reader["Descrizione"].ToString(),
                        Convert.ToInt32(reader["PuntiDecurtati"]),
                        Convert.ToDecimal(reader["Importo"]),
                        Convert.ToBoolean(reader["Contestabile"].ToString())
                        );

                    listaViolazioni.Add(violazione);
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

            return View(listaViolazioni);
        }
        
    }
}