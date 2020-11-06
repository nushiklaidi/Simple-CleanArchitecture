using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.MVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> GetAll()
        {
            var model = await _bookService.GetBooks();
            return View(model);
        }

        public async Task<IActionResult> BookDetail(int id) 
        {
            var model = await _bookService.GetBookById(id);
            return View(model);
        }
    }
}
