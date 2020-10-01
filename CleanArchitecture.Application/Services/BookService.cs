using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _uow;

        public BookService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Book GetBookById(int id)
        {
            var getBookById = _uow.BookRepository.GetById(id);
            return getBookById;
        }

        public IEnumerable<Book> GetBooks()
        {
            var model = _uow.BookRepository.GetAll();
            return model;
        }
    }
}
