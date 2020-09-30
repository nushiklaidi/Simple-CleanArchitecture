using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.MVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public ActionResult<IEnumerable<Book>> GetAll() => _bookService.GetBooks().ToList();

        public ActionResult<Book> BookDetail(int id) 
        {
            return _bookService.GetBookById(id);
        }
    }
}
