using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        #region Private Repository
        public IBookRepository _bookRepository { get; private set; }
        #endregion

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IBookRepository BookRepository => _bookRepository = _bookRepository ?? new BookRepository(_appDbContext);
    }
}
