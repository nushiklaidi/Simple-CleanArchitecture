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
        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter,
            string includeProperties = "",
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? pageNumber = null,
            int? recordsPerPage = null,
            bool GetRowCount = false
            );

        IEnumerable<T> Get(
              Expression<Func<T, bool>> filter,
              string[] includeProperties,
              Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
              int? pageNumber = null,
              int? recordsPerPage = null,
              bool GetRowCount = false
              );
        T GetById(object id);

        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
        void Delete(T entity);
    }
}
