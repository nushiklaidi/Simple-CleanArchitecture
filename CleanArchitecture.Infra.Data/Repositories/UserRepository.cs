using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Model;
using CleanArchitecture.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Infra.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }
    }
}
