using CleanArchitecture.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooks();
        Book GetBookById(int id);
    }
}
