using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentieTurismC.Domain;
using AgentieTurismC.Repository;

namespace AgentieTurismC
{
    public class ServiceExcursie
    {
        ExcursieRepository excursieRepository;

        public ServiceExcursie(ExcursieRepository excursieRepository)
        {
            this.excursieRepository = excursieRepository;
        }
        public List<Excursie> getAll()
        {
            return excursieRepository.GetAll();
        }
        public List<Excursie> getAllByLandmarkAndInterval(String landmark, TimeOnly t1, TimeOnly t2)
        {
            return excursieRepository.GetByLandmarkAndInterval(landmark, t1, t2);
        }
    }
}
