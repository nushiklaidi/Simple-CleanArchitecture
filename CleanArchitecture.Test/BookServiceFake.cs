using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.Test
{
    public class BookServiceFake : IBookService
    {
        private readonly List<Book> _books;

        public BookServiceFake()
        {
            _books = new List<Book>()
            {
                new Book()
                {
                    Id = 10,
                    ISBN = "NO10",
                    AuthorName = "Nushi Klaidi",
                    Name = "Title Book"
                },
                new Book()
                {
                    Id = 11,
                    ISBN = "NO11",
                    AuthorName = "Nushi Klaidi1",
                    Name = "Title Book1"
                }
            };
        }

        public Book GetBookById(int id)
        {
            var getBookById = _books.Where(b => b.Id == id);
            return getBookById.FirstOrDefault();
        }

        public IEnumerable<Book> GetBooks()
        {
            return _books;
        }
    }
}
