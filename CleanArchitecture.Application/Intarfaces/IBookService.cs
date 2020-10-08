using CleanArchitecture.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Intarfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBookById(int id);
        Task Delete(int id);
        Task Activate(int bookId, bool active);
    }
}
