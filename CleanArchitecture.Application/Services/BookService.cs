using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _uow;

        public BookService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Book> GetBookById(int id)
        {
            return await Find(id);
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _uow.BookRepository.GetAll();
        }
        public async Task Delete(int id)
        {
            var model = await Find(id);
            await _uow.BookRepository.Delete(model);
        }
               
        public async Task Activate(int bookId, bool active)
        {
            var model = await Find(bookId);
            model.Active = active;
            await _uow.SavechangesAsync();
        }

        private async Task<Book> Find(int id)
        {
            return await _uow.BookRepository.GetById(id);
        }
    }
}
