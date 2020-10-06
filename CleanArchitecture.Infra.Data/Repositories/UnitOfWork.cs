﻿using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        public IBookRepository _bookRepository { get; private set; }
        public IUserRepository _userRepository { get; private set; }
        
        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public int SaveChanges()
        {
            return _appDbContext.SaveChanges();
        }

        public Task<int> SavechangesAsync()
        {
            return _appDbContext.SaveChangesAsync();
        }

        public IBookRepository BookRepository => _bookRepository = _bookRepository ?? new BookRepository(_appDbContext);
        public IUserRepository UserRepository => _userRepository = _userRepository ?? new UserRepository(_appDbContext);     
    }
}
