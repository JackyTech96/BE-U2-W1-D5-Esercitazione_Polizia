using Esercitazione_Polizia.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Esercitazione_Polizia.Controllers
{
    public class TrasgressoreController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["PoliziaDB"].ConnectionString;

        // GET: Trasgressore
        public ActionResult Index()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            List<Trasgressore> listaTrasgressori = new List<Trasgressore>();

            try
            {
                conn.Open();

                string query = "SELECT * FROM Trasgressori";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Trasgressore trasgressore = new Trasgressore
                    {
                        IDTrasgressore = Convert.ToInt32(reader["IDTrasgressore"]),
                        Nome = reader["Nome"].ToString(),
                        Cognome = reader["Cognome"].ToString(),
                        CF = reader["CF"].ToString(),
                        Citta = reader["Citta"].ToString(),
                        Indirizzo = reader["Indirizzo"].ToString(),
                        CAP = Convert.ToInt32(reader["CAP"])
                    };

                    listaTrasgressori.Add(trasgressore);
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

            return View(listaTrasgressori);
        }



        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Trasgressore nuovoTrasgressore)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                string query = "INSERT INTO Trasgressori (Nome, Cognome, CF, Citta, Indirizzo, CAP) " +
                               "VALUES (@Nome, @Cognome, @CF, @Citta, @Indirizzo, @CAP)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nome", nuovoTrasgressore.Nome);
                cmd.Parameters.AddWithValue("@Cognome", nuovoTrasgressore.Cognome);
                cmd.Parameters.AddWithValue("@CF", nuovoTrasgressore.CF);
                cmd.Parameters.AddWithValue("@Citta", nuovoTrasgressore.Citta);
                cmd.Parameters.AddWithValue("@Indirizzo", nuovoTrasgressore.Indirizzo);
                cmd.Parameters.AddWithValue("@CAP", nuovoTrasgressore.CAP);

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