using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(string include);
        T GetById(object id);

        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
        void Delete(T entity);
    }
}
