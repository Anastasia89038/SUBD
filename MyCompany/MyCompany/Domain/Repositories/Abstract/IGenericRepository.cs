using MyCompany.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.Domain.Repositories.Abstract
{
    public interface IGenericRepository<T> where T: EntityBase
    {
        List<T> GetAll();
        T Get(string id);
        void Update(string id, T entity);
        void Delete(string id);
        void Create(T entity);
    }
}
