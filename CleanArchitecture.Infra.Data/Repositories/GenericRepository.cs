using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CleanArchitecture.Infra.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> dbSet;
        private int _totalNumberOfQueryRecords = 0;

        public int TotalNumberOfQueryRecords { get { return _totalNumberOfQueryRecords; } }

        public GenericRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
            this.dbSet = _appDbContext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            List<T> query = dbSet.ToList<T>();
            return query;
        }

        public IEnumerable<T> GetAll(string includeProperties)
        {
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                var arrProperties = includeProperties.Split(',');
                return GetQuery(includeProperties: arrProperties).ToList<T>();
            }
            else
            {
                List<T> query = dbSet.ToList<T>();
                return query;
            }
        }

        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter,
            string[] includeProperties,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? pageNumber = null,
            int? recordsPerPage = null,
            bool GetRowCount = false
            )
        {
            return GetQuery(filter, includeProperties, orderBy, pageNumber, recordsPerPage, GetRowCount).ToList();
        }

        public virtual IEnumerable<T> Get(
           Expression<Func<T, bool>> filter,
           string includeProperties = "",
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           int? pageNumber = null,
           int? recordsPerPage = null,
           bool GetRowCount = false
           )
        {

            var properties = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return Get(filter, properties, orderBy, pageNumber, recordsPerPage, GetRowCount).ToList();
        }

        public T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            _appDbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        protected virtual IQueryable<T> GetQuery(
              Expression<Func<T, bool>> filter = null,
              string[] includeProperties = null,
              Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
              int? pageNumber = null,
              int? recordsPerPage = null,
              bool GetRowCount = false
              )
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
                query = query.Where(filter);
            //Warning: Very expensive operation!!!
            if (GetRowCount)
            {
                IQueryable<T> queryCount = query;
                _totalNumberOfQueryRecords = queryCount.Count();
            }
            else
                _totalNumberOfQueryRecords = -1;
            //Included properties
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);

            if (orderBy != null)
            {
                query = orderBy(query);
                //Skip supported only to sorted rows
                if (pageNumber != null && recordsPerPage != null)
                    query = query.Skip((pageNumber.Value - 1) * recordsPerPage.Value).Take(recordsPerPage.Value);
            }
            return query;
        }
    }
}
