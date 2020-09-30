using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Model;
using CleanArchitecture.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.Infra.Data.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context) : base(context)
        {
            //_context = context;
        }

        //public Book GetBookById(int id)
        //{
        //    return _context.Books.Where(b => b.Id == id).FirstOrDefault();
        //}

        //public IEnumerable<Book> GetBooks()
        //{
        //    return _context.Books;
        //}
    }
}
