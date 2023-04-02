using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentieTurismC.Domain
{
    public class Rezervare: Entity
    {
        //Change the caseing of the variables
        public String name { get; set; }
        public String phoneNumber { get; set; }
        public int numberOfTickets { get; set; }

        public Rezervare(string name, string phone_number, int number_of_tickets)
        {
            this.name = name;
            this.phoneNumber = phone_number;
            this.numberOfTickets = number_of_tickets;
        }
    }
}
