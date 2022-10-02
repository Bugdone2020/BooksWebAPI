using BooksWebAPI_BL.DTOs;
using System;
using System.Threading.Tasks;

namespace BooksWebAPI_BL.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> SignIn(string login, string password);
        Task<Guid> SignUp(UserDto user);
        Task<bool> ConfirmUserMail(string encryptedEmail);
    }
}