using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using AgentieTurismC.Domain;

namespace AgentieTurismC.Repository
{
    public interface IRepository <T> where T : Entity
    {
        T GetById(int id);
        List<T> GetAll();
        void Save(T entity);
        void Update(int id, T entity);
        void Delete(int id);
    }
}
