using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentieTurismC.Domain
{
    public class Excursie: Entity
    {
        public String landmark { get; set; }
        public String transportCompany { get; set; }
        public TimeOnly departureTime { get; set; }
        public int availableTickets { get; set; }

        public double pret { get; set; }


        public Excursie(string landmark, string transportCompany, TimeOnly departureTime, int availableTickets, double pret)
        {
            this.landmark = landmark;
            this.transportCompany = transportCompany;
            this.departureTime = departureTime;
            this.availableTickets = availableTickets;
            this.pret = pret;
        }

        public override bool Equals(object? obj)
        {
            return obj is Excursie excursie &&
                   Id == excursie.Id &&
                   landmark == excursie.landmark &&
                   transportCompany == excursie.transportCompany &&
                   departureTime.Equals(excursie.departureTime) &&
                   availableTickets == excursie.availableTickets &&
                   pret == excursie.pret;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, landmark, transportCompany, departureTime, availableTickets, pret);
        }
        public override string ToString()
        {
            return "Excursie la " + landmark + " cu " + transportCompany + " de la ora |"+departureTime+"| pentru " + availableTickets + " persoane | Pret: " + pret;
        }
    }
}
