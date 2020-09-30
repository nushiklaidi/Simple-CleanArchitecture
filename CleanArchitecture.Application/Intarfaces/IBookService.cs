using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Intarfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooks();
        Book GetBookById(int id);
    }
}
