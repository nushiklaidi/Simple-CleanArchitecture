using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Application.Model;
using CleanArchitecture.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = AppConst.Role.AdminRole)]
    public class BookController : ControllerBase
    {
        #region Properties

        private readonly IBookService _bookService;

        #endregion

        #region Contructor

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(await _bookService.GetBooks());

        /// <summary>
        /// Get book by id
        /// </summary>
        /// <param name="id">The db identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
                return BadRequest(new { message = "The Id doesn't exist" });
            return Ok(book);
        }

        /// <summary>
        /// Active or Deactive book
        /// </summary>
        /// <param name="id"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Activate(BookViewModel model)
        {
            if(model == null)
                return BadRequest(new { message = "The object is null" });
            await _bookService.Activate(bookId: model.Id, active: model.Active);
            return Ok();
        }

        #endregion

    }
}
