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
    public class VerbaleController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["PoliziaDB"].ConnectionString;

        // GET: Verbale
        public ActionResult Index()
        {

            SqlConnection conn = new SqlConnection(connectionString);
            List<Verbale> listaVerbali = new List<Verbale>();

            try
            {
                conn.Open();

                string query = "SELECT * FROM Verbali";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Verbale verbale = new Verbale
                    {
                        IDVerbale = Convert.ToInt32(reader["IDVerbale"]),
                        IDTrasgressore = Convert.ToInt32(reader["IDTrasgressore"]),
                        IDViolazione = Convert.ToInt32(reader["IDViolazione"]),
                        DataViolazione = Convert.ToDateTime(reader["DataViolazione"]),
                        Nominativo_Agente = reader["Nominativo_Agente"].ToString(),

                    };

                    listaVerbali.Add(verbale);
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

            return View(listaVerbali);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Verbale nuovoVerbale)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                string query = "INSERT INTO Verbali (IDTrasgressore, IDViolazione, DataViolazione, Nominativo_Agente) " +
                               "VALUES (@IDTrasgressore, @IDViolazione, @Data_Violazione, @Nominativo_Agente)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IDTrasgressore", nuovoVerbale.IDTrasgressore);
                cmd.Parameters.AddWithValue("@IDViolazione", nuovoVerbale.IDViolazione);
                cmd.Parameters.AddWithValue("@Data_Violazione", nuovoVerbale.DataViolazione);
                cmd.Parameters.AddWithValue("@Nominativo_Agente", nuovoVerbale.Nominativo_Agente);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write($"Si è verificato un errore: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }

            return View();
        }

        
    }
}