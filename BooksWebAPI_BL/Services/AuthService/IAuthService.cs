using System.Threading.Tasks;

namespace BooksWebAPI_BL.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> SignIn(string login, string password);
    }
}