using BooksWebAPI_BL.DTOs;
using BooksWebAPI_BL.Services.AuthService;
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
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<UsersController> _logger;
        public UsersController(
            IAuthService authService, 
            ILogger<UsersController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmUserMail(string email)
        {
            throw new ArgumentException();
            return Ok(await _authService.ConfirmUserMail(email));
        }

        [HttpGet]
        public async Task<IActionResult> SignIn(string login, string password)
        {
            _logger.LogInformation("Trying to login");
            string token = null;
            try
            {
                token = await _authService.SignIn(login, password);
            }
            catch(ArgumentException)
            {
            }

            return !string.IsNullOrEmpty(token) ? Ok(token) 
                                                : Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserDto userDto)
        {
            return Ok(await _authService.SignUp(userDto));
        }
    }
}
