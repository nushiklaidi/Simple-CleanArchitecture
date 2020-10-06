using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CleanArchitecture.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(string includeProperties);
        IEnumerable<T> Get(Expression<Func<T, bool>> filter, string includeProperties = "");
        IEnumerable<T> Get(Expression<Func<T, bool>> filter, string[] includeProperties);
        T GetById(object id);

        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
        void Delete(T entity);
    }
}
