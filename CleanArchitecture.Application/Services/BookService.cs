using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Model;
using System.Collections.Generic;

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
        public void Delete(int id)
        {
            var model = Find(id);
            _uow.BookRepository.Delete(model);
            _uow.SaveChanges();
        }
               
        public void Activate(int bookId, bool active)
        {
            var model = Find(bookId);
            model.Active = active;
            _uow.SaveChanges();
        }

        private Book Find(int id)
        {
            var model = _uow.BookRepository.GetById(id);
            return model;
        }
    }
}
