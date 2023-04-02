using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AgentieTurismC.Domain;
using Npgsql;

namespace AgentieTurismC.Repository
{
    public class AgentieRepository : IRepository<Agentie>
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase
        .GetCurrentMethod()?.DeclaringType);
        private NpgsqlConnection connection;
        public AgentieRepository()
        {
            connection = new NpgsqlConnection(Helper.CnnVal("agentieDB"));
            logger.Info("Logging to database using: " + Helper.CnnVal("agentieDB"));
            connection.Open();
            logger.Info("~ Connected ~");
        }
        public void Delete(int id)
        {
            logger.Info("Deleting agentie with id " + id);
            string commandText = $"DELETE FROM agentii WHERE id = @id";
            using (var cmd = new NpgsqlCommand(commandText, connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
            logger.Info("deleted agentie!");
        }

        public List<Agentie> GetAll()
        {
            logger.Info("Looking for all agencies!");
            List<Agentie> agentii = new List<Agentie>();
            string commandText = $"SELECT * FROM agentii";
            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        var ID = (Int32)reader["id"];
                        string name = reader["name"] as string;
                        string username = reader["username"] as string;
                        string password = reader["password"] as string;

                        //Console.WriteLine(ID + name + username + password);
                        Agentie admin = new Agentie(name, username, password);
                        agentii.Add(admin);
                    }
            }
            logger.Info("Done!");
            return agentii;
        }

        public Agentie GetById(int id)
        {
            logger.Info("Looking for agentie with id = " + id);
            string commandText = $"SELECT * FROM agentii WHERE id = @id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
            {
                cmd.Parameters.AddWithValue("id", id);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    if (reader.Read())
                    {
                        var j = (Int32)reader["id"];
                        var name = reader["name"] as string;
                        var username = reader["username"] as string;
                        var password = reader["password"] as string;

                        Agentie agentie= new Agentie(name, username, password);
                        logger.Info("Found agentie");
                        return agentie;
                    }
            }
            logger.Info("Didn't find it!");
            return null;
        }

        public void Save(Agentie entity)
        {
            logger.Info("Saving agentie " + entity + " ....");
            string commandText = $"INSERT INTO agentii (name, username, password) VALUES (@name, @username, @password)";
            using (var cmd = new NpgsqlCommand(commandText, connection))
            {
                cmd.Parameters.AddWithValue("name", entity.name);
                cmd.Parameters.AddWithValue("username", entity.username);
                cmd.Parameters.AddWithValue("password", entity.password);

                cmd.ExecuteNonQuery();
            }
            logger.Info("Saved agentie " + entity + "!");
        }

        public void Update(int id, Agentie entity)
        {
            logger.Info("Updating agentie id" + id + " for " + entity);
            string commandText = $"UPDATE agentii SET name = @name, username = @username, password = @password WHERE id = @id";
            using (var cmd = new NpgsqlCommand(commandText, connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("name", entity.name);
                cmd.Parameters.AddWithValue("username", entity.username);
                cmd.Parameters.AddWithValue("password", entity.password);

                cmd.ExecuteNonQuery();
            }
            logger.Info("Succesful update!");
        }
        
    }
}
