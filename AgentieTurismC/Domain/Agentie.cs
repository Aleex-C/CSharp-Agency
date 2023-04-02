using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentieTurismC.Domain
{
    public class Agentie: Entity
    {
        public String name { get; set; }
        public String username { get; set; }
        public String password { get; set; }

        public Agentie(string name, string username, string password)
        {
            this.name = name;
            this.username = username;
            this.password = password;
        }
        public override string ToString()
        {
            return "Agentie: " + name + " | User:  " + username;
        }
    }
}
