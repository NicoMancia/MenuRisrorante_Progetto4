using MenuRistorante.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MenuRistorante.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : ControllerBase
    {

        SqlConnection connection = new SqlConnection
            ("Data Source=nickfabserver.database.windows.net;Initial " +
                "Catalog=NickFabDBSQL;Persist Security Info=True;User ID=CloudSA88793a09;" +
                "Password=NickFabDBSQL0102");

        [HttpGet (Name = "Visualizza MENÙ")]
        public IEnumerable<Piatto> VisualizzaMenù ()
        {
            List<Piatto> Piatti = new List<Piatto>();
            connection.Open();
            string Query = "SELECT Portata.NomePortata, Piatto.IDPiatto, Piatto.NomePiatto, Piatto.Prezzo " +
                "FROM Piatto,Portata " +
                "WHERE Piatto.IdPortata = Portata.IDPortata " +
                "ORDER BY Piatto.IdPortata";

            SqlCommand command = new SqlCommand(Query, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Piatto piatto = new Piatto(
                        Convert.ToInt32(reader["IDPiatto"]),
                        reader["NomePiatto"].ToString(),
                        Convert.ToDecimal(reader["Prezzo"]),
                        reader["NomePortata"].ToString()
                        );

                    Piatti.Add(piatto);
                }
            }
            return Piatti;
        }

        [HttpGet(Name = "Visualizza PORTATE")]
        public IEnumerable<Portata> VisualizzaPortate()
        {
            List<Portata> Portate = new List<Portata>();
            connection.Open();
            string Query = "SELECT * FROM Portata";

            SqlCommand command = new SqlCommand(Query, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Portata Portata = new Portata(
                        Convert.ToInt32(reader["IDPortata"]),
                        reader["NomePortata"].ToString()
                        );
                    Portate.Add(Portata);
                }
            }
            return Portate;
        }

        [HttpGet(Name = "Visualizza PIATTI")]
        public IEnumerable<Piatto> VisualizzaPiatti()
        {
            List<Piatto> Piatti = new List<Piatto>();
            connection.Open();
            string Query = "SELECT Portata.NomePortata, Piatto.IDPiatto, Piatto.NomePiatto, Piatto.Prezzo " +
                "FROM Piatto,Portata " +
                "WHERE Piatto.IdPortata = Portata.IDPortata ";

            SqlCommand command = new SqlCommand(Query, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Piatto piatto = new Piatto(
                        Convert.ToInt32(reader["IDPiatto"]),
                        reader["NomePiatto"].ToString(),
                        Convert.ToDecimal(reader["Prezzo"]),
                        reader["NomePortata"].ToString()
                        );

                    Piatti.Add(piatto);
                }
            }
            return Piatti;
        }

        [HttpPost(Name = "Inserisci PORTATA")]
        public IActionResult CreaPortata(string nome)
        {
            connection.Open();
            string Query = "INSERT INTO Portata (NomePortata) VALUES ('" + nome + "')";

            SqlCommand command = new SqlCommand(Query, connection);

            command.ExecuteNonQuery();

            connection.Close();
            return Ok("Portata creata con successo!");
        }

        [HttpPost(Name = "Inserisci PIATTO")]
        public IActionResult CreaPiatto(string nome, decimal prezzo, string NomePortata)
        {
            connection.Open();

            string QueryVerifica = "SELECT IDPortata, NomePortata FROM Portata WHERE NomePortata = '" + NomePortata + "'";

            SqlCommand commandVerifica = new SqlCommand(QueryVerifica, connection);
            SqlDataReader reader = commandVerifica.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();

                string Query = "INSERT INTO Piatto (NomePiatto, Prezzo, IdPortata) " +
                       "VALUES ('" + nome + "'," + prezzo + "," + Convert.ToInt32(reader["IDPortata"]) + ")";

                SqlCommand command = new SqlCommand(Query, connection);

                reader.Close();
                command.ExecuteNonQuery();

            }
            else
            {
                return Ok("Inserisci una portata valida!");
            }

            connection.Close();
            return Ok("Piatto creato con successo!");
        }

        [HttpPut(Name = "Modifica PORTATA")]
        public IActionResult ModificaPortata(int IDModificare, string nomeSostitutivo)
        {
            connection.Open();

            string Query = "UPDATE Portata SET NomePortata = '" + nomeSostitutivo + "' WHERE IDPortata = " + IDModificare;

            SqlCommand command = new SqlCommand(Query, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.RecordsAffected != 0)
            {
                connection.Close();
                return Ok("Portata modificata con successo!");
            }
            return Ok("Portata non trovata, inserisci un ID valido!");
        }

        [HttpPut(Name = "Modifica PIATTO")]
        public IActionResult ModificaPiatto(int IDModificare, string nomeSostitutivo, decimal prezzoSostitutivo)
        {
            connection.Open();

            string Query = "UPDATE Piatto SET NomePiatto = '" + nomeSostitutivo + "', Prezzo = " + prezzoSostitutivo + " WHERE IDPiatto = " + IDModificare;

            SqlCommand command = new SqlCommand(Query, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.RecordsAffected!=0)
            { 
                connection.Close();
                return Ok("Piatto modificiato con successo!");                
            }
            return Ok("Piatto non trovato, inserisci un ID valido!");            
        }

        [HttpDelete(Name = "Elimina PORTATA")]
        public IActionResult EliminaPortata(int IDEliminare)
        {
            connection.Open();
            string Query = "DELETE From Portata where IDPortata = " + IDEliminare;

            SqlCommand command = new SqlCommand(Query, connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.RecordsAffected != 0)
            {
                connection.Close();
                return Ok("Portata eliminata con successo!");
            }
            return Ok("Portata non trovata, inserisci un ID valido!");
        }

        [HttpDelete(Name = "Elimina PIATTO")]
        public IActionResult EliminaPiatto(int IDEliminare)
        {
            connection.Open();
            string Query = "DELETE From Piatto where IDPiatto = " + IDEliminare;

            SqlCommand command = new SqlCommand(Query, connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.RecordsAffected != 0)
            {
                connection.Close();
                return Ok("Piatto Eliminato con successo!");
            }
            return Ok("Piatto non trovato, inserisci un ID valido!");
        }
    }
}
