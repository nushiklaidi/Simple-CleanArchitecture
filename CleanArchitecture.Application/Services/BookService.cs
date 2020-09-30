using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Book GetBookById(int id)
        {
            var getBookById = _bookRepository.GetBookById(id);
            return getBookById;
        }

        public IEnumerable<Book> GetBooks()
        {
            var model = _bookRepository.GetBooks();
            return model;
        }
    }
}
