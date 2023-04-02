using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentieTurismC.Domain;
using Npgsql;

namespace AgentieTurismC.Repository
{
    public class RezervareRepository : IRepository<Rezervare>
    {
        private NpgsqlConnection connection;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase
        .GetCurrentMethod()?.DeclaringType);
        public RezervareRepository()
        {
            connection = new NpgsqlConnection(Helper.CnnVal("agentieDB"));
            logger.Info("Logging to database using: " + Helper.CnnVal("agentieDB"));
            connection.Open();
            logger.Info("~ Connected ~");
        }
        public void Delete(int id)
        {
            logger.Info("Deleting rezervare with id =" + id);
            string commandText = $"DELETE FROM rezervari WHERE id = @id";
            using (var cmd = new NpgsqlCommand(commandText, connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
            logger.Info("Deleted!");
        }

        public List<Rezervare> GetAll()
        {
            List<Rezervare> rezervari = new List<Rezervare>();
            string commandText = $"SELECT * FROM rezervari";
            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        string name = reader["name"] as string;
                        string phoneNumber = reader["phone_number"] as string;
                        int numberOfTickets = (Int32) reader["number_of_tickets"];

                        //Console.WriteLine(ID + name + username + password);
                        Rezervare rezervare = new Rezervare(name, phoneNumber, numberOfTickets);
                        rezervari.Add(rezervare);
                    }
            }
            return rezervari;
        }

        public Rezervare GetById(int id)
        {
            string commandText = $"SELECT * FROM rezervari WHERE id = @id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
            {
                cmd.Parameters.AddWithValue("id", id);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    if (reader.Read())
                    {
                        string name = reader["name"] as string;
                        string phoneNumber = reader["phone_number"] as string;
                        int numberOfTickets = (Int32) reader["number_of_tickets"];

                        Rezervare rezervare = new Rezervare(name, phoneNumber, numberOfTickets);
                        return rezervare;
                    }
            }
            return null;
        }

        public void Save(Rezervare entity)
        {
            logger.Info("Saving  rezervare! ... " + entity);
            string commandText = $"INSERT INTO rezervari (name, phone_number, number_of_tickets) VALUES (@name, @phone_number, @number_of_tickets)";
            using (var cmd = new NpgsqlCommand(commandText, connection))
            {
                cmd.Parameters.AddWithValue("name", entity.name);
                cmd.Parameters.AddWithValue("phone_number", entity.phoneNumber);
                cmd.Parameters.AddWithValue("number_of_tickets", entity.numberOfTickets);

                cmd.ExecuteNonQuery();
            }
            logger.Info("Saved!");
        }

        public void Update(int id, Rezervare entity)
        {
            logger.Info("Updating ...");
            string commandText = $"UPDATE rezervari SET name = @name, phone_number = @phone_number, number_of_tickets = @number_of_tickets WHERE id = @id";
            using (var cmd = new NpgsqlCommand(commandText, connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("name", entity.name);
                cmd.Parameters.AddWithValue("phone_number", entity.phoneNumber);
                cmd.Parameters.AddWithValue("number_of_tickets", entity.numberOfTickets);

                cmd.ExecuteNonQuery();
            }
            logger.Info("Updated!");
        }
        
    }
}
