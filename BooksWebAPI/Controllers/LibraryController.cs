using BooksWebAPI_BL.Services.LibraryService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLibraries(float latitude, float longitude)
        {
            return Ok(
                await _libraryService.GetNearestLibraries(
                    new BooksWebAPI_DAL.Entities.Point()
                    {
                        Latitude = latitude,
                        Longitude = longitude
                    }));
        }
    }
}
