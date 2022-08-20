using BooksWebAPI_BL;
using BooksWebAPI_DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService, ILogger<BooksController> logger)
        {
            _booksService = booksService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Book> GetAllBooks()
        {
            return _booksService.GetAllBooks();
        }

        [HttpGet("{id}")]
        public Book GetBookById(Guid id)
        {
            return _booksService.GetBookById(id);
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            try
            {
                var result = _booksService.AddBook(book);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public bool UpdateBook(Guid id, Book book)
        {
            book.Id = id;
            return _booksService.UpdateBook(book);
        }

        [HttpDelete("{id}")]
        public bool DeleteBook(Guid id)
        { 
            return _booksService.DeleteBookById(id);
        }
    }
}
