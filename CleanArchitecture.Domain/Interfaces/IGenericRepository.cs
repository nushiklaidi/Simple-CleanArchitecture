using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
    }
}
