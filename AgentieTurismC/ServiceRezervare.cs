using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentieTurismC.Domain;
using AgentieTurismC.Repository;

namespace AgentieTurismC
{
    public class ServiceRezervare
    {
        RezervareRepository rezervareRepository;

        public ServiceRezervare(RezervareRepository rezervareRepository)
        {
            this.rezervareRepository = rezervareRepository;
        }
        public void Save(String name, String phoneNo, int numberOfTickets)
        {
            rezervareRepository.Save(new Rezervare(name, phoneNo, numberOfTickets));
        }
    }
}
