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
    public class ExcursieRepository : IRepository<Excursie>
    {
        private NpgsqlConnection connection;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase
        .GetCurrentMethod()?.DeclaringType);
        public ExcursieRepository()
        {
            connection = new NpgsqlConnection(Helper.CnnVal("agentieDB"));
            logger.Info("Logging to database using: " + Helper.CnnVal("agentieDB"));
            connection.Open();
            logger.Info("~ Connected ~");
        }
        public void Delete(int id)
        {
            logger.Info("Deleting excursie with id " + id);
            string commandText = $"DELETE FROM excursii WHERE id = @id";
            using (var cmd = new NpgsqlCommand(commandText, connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
            logger.Info("deleted excursie!");
        }

        public List<Excursie> GetAll()
        {
            logger.Info("Looking for all excursii!");
            List<Excursie> excursii = new List<Excursie>();
            string commandText = $"SELECT * FROM excursii";
            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        var ID = (Int32)reader["id"];
                        string landmark = reader["landmark"] as string;
                        string transportComanpy = reader["transport_company"] as string;
                        TimeOnly departureTime = TimeOnly.FromTimeSpan((TimeSpan) reader["departure_time"]);
                        int availableTickets = (Int32)reader["available_tickets"];
                        double price = (double)reader["price"];

                        //Console.WriteLine(ID + name + username + password);
                        Excursie excursie = new Excursie(landmark, transportComanpy, departureTime, availableTickets, price);
                        excursii.Add(excursie);
                    }
            }
            logger.Info("Succesful looking!");
            return excursii;
        }
        public List<Excursie> GetByLandmarkAndInterval(string landmark_find, TimeOnly t1, TimeOnly t2)
        {
            logger.Info("Looking for " + landmark_find + " in the " + t1 + " -- " + t2 + " interval!");
            List<Excursie> excursii = new List<Excursie>();
            string commandText = $"SELECT * FROM excursii WHERE landmark = @landmark AND departure_time > @t1 AND departure_time < @t2";
            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
            {
                cmd.Parameters.AddWithValue("landmark", landmark_find);
                cmd.Parameters.AddWithValue("t1", t1);
                cmd.Parameters.AddWithValue("t2", t2);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        var ID = (Int32)reader["id"];
                        string landmark = reader["landmark"] as string;
                        string transportComanpy = reader["transport_company"] as string;
                        TimeOnly departureTime = TimeOnly.FromTimeSpan((TimeSpan)reader["departure_time"]);
                        int availableTickets = (Int32)reader["available_tickets"];
                        double price = (double)reader["price"];

                        //Console.WriteLine(ID + name + username + password);
                        Excursie excursie = new Excursie(landmark, transportComanpy, departureTime, availableTickets, price);
                        excursii.Add(excursie);
                    }
            }
            logger.Info("Found them!" + excursii);
            return excursii;
        }

        public Excursie GetById(int id)
        {
            logger.Info("Looking for the excursie with id = " + id);
            string commandText = $"SELECT * FROM excursii WHERE id = @id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
            {
                cmd.Parameters.AddWithValue("id", id);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    if (reader.Read())
                    {
                        var ID = (Int32)reader["id"];
                        string landmark = reader["landmark"] as string;
                        string transportComanpy = reader["transport_company"] as string;
                        TimeOnly departureTime = TimeOnly.FromTimeSpan((TimeSpan)reader["departure_time"]);
                        int availableTickets = (Int32)reader["available_tickets"];
                        double price = (double) reader["price"];

                        Excursie excursie = new Excursie(landmark, transportComanpy, departureTime, availableTickets, price);
                        logger.Info("Found it!");
                        return excursie;
                    }
            }
            logger.Info("Didn't find it!");
            return null;
        }

        public void Save(Excursie entity)
        {
            logger.Info("Saving excursie " + entity + " ....");
            string commandText = $"INSERT INTO excursii (landmark, transport_company, departure_time, available_tickets, price) VALUES (@landmark, @transport_company, @departure_time, " +
                $"@available_tickets, @price)";
            using (var cmd = new NpgsqlCommand(commandText, connection))
            {
                cmd.Parameters.AddWithValue("landmark", entity.landmark);
                cmd.Parameters.AddWithValue("transport_company", entity.transportCompany);
                cmd.Parameters.AddWithValue("departure_time", entity.departureTime);
                cmd.Parameters.AddWithValue("available_tickets", entity.availableTickets);
                cmd.Parameters.AddWithValue("price", entity.pret);

                cmd.ExecuteNonQuery();
            }
            logger.Info("Saved excursie " + entity + "!");
        }

        public void Update(int id, Excursie entity)
        {
            logger.Info("Updating excursie id" + id + " for " + entity);
            string commandText = $"UPDATE excursii SET landmark = @landmark, transport_company = @transport_company, departure_time = @departure_time, available_tickets = @available_tickets, price = @price  WHERE id = @id";
            using (var cmd = new NpgsqlCommand(commandText, connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("landmark", entity.landmark);
                cmd.Parameters.AddWithValue("transport_company", entity.transportCompany);
                cmd.Parameters.AddWithValue("departure_time", entity.departureTime);
                cmd.Parameters.AddWithValue("available_tickets", entity.availableTickets);
                cmd.Parameters.AddWithValue("price", entity.pret);

                cmd.ExecuteNonQuery();
            }
            logger.Info("Succesful update!");
        }
    }
}
