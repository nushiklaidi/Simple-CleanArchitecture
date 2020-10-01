using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanArchitecture.Infra.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> dbSet;

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

        public IEnumerable<T> GetAll(string include)
        {
            var arrInclude = include.Split(',');
            return GetQuery(include: arrInclude).ToList<T>();
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

        protected virtual IQueryable<T> GetQuery(string[] include)
        {
            IQueryable<T> query = dbSet;

            foreach (var includeProperty in include)
                query = query.Include(includeProperty);
            return query;
        }
    }
}
