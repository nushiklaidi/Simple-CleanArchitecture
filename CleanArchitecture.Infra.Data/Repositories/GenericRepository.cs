﻿using CleanArchitecture.Domain.Interfaces;
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
        private readonly AppDbContext _context;
        private readonly DbSet<T> dbSet;

        public GenericRepository(AppDbContext appDbContext)
        {
            this._context = appDbContext;
            this.dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            List<T> query = dbSet.ToList<T>();
            return query;
        }

        public T GetById(object id)
        {
            return dbSet.Find(id);
        }
    }
}