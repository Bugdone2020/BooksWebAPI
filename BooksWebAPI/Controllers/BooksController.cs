using BooksWebAPI_BL;
using BooksWebAPI_DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksWebAPI_DAL.Entities;
using BooksWebAPI_BL.Services.BookService;
using BooksWebAPI_BL.DTOs;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize(Roles = BooksWebAPI_BL.Roles.Reader)]
        [HttpGet]
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return  await _booksService.GetAllBooks();
        }

        [HttpGet("{id}")]
        public async Task<Book> GetBookById(Guid id)
        {
            return await _booksService.GetBookById(id);
        }

        [HttpGet("full/{id}")]
        public async Task<BookDto> GetFullInfoById(Guid id)
        {
            return await _booksService.GetBookFullInfo(id);
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
        public async Task<IActionResult> UpdateBook(Guid id, Book book)
        {
            book.Id = id;

            var result = await _booksService.UpdateBook(book);

            return result ? StatusCode(200) : StatusCode(400);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteBook(Guid id)
        { 
            return await _booksService.DeleteBookById(id);
        }
    }
}
