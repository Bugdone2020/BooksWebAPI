using BooksWebAPI_BL.Auth;
using BooksWebAPI_BL.DTOs;
using BooksWebAPI_BL.Services.EncryptionService;
using BooksWebAPI_BL.Services.HashService;
using BooksWebAPI_BL.Services.SMTPService;
using BooksWebAPI_DAL;
using BooksWebAPI_DAL.Entities;
using System;
using System.Threading.Tasks;

namespace BooksWebAPI_BL.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly ISendingBlueSmtpService _sendingBlueSmtpService;
        private readonly IGenericRepository<User> _genericClientRepository;
        private readonly IGenericRepository<Role> _genericRoleRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IHashService _hashService;
        private readonly IEncryptionService _encryptionService;
        public AuthService(
            IEncryptionService encryptionService,
            ISendingBlueSmtpService sendingBlueSmtpService,
            IGenericRepository<User> genericClientRepository,
            IGenericRepository<Role> genericRoleRepository,
            ITokenGenerator tokenGenerator,
            IHashService hashService)
        {
            _encryptionService = encryptionService;
            _sendingBlueSmtpService = sendingBlueSmtpService;
            _genericClientRepository = genericClientRepository;
            _genericRoleRepository = genericRoleRepository;
            _tokenGenerator = tokenGenerator;
            _hashService = hashService;
        }

        public async Task<string> SignIn(string login, string password)
        {
            var user = await _genericClientRepository.GetByPredicate(
                x => x.Email == login && x.Password == _hashService.HashString(password));

            if (user == null)
            {
                throw new ArgumentException();
            }

            var role = user.RoleId.HasValue ? (await _genericRoleRepository.GetById(user.RoleId.Value)).Name
                                            : Roles.Reader;

            return _tokenGenerator.GenerateToken(user.Email, role);
        }

        public async Task<Guid> SignUp(UserDto user)
        {
            user.Password = _hashService.HashString(user.Password);

            var response = await _genericClientRepository.Add(Map(user));

            await _sendingBlueSmtpService.SendMail(
                new MailInfo
                {
                    ClientName = $"{user.FirstName} {user.LastName}",
                    Email = user.Email,
                    Subject = "Email confirmation",
                    Body = "https://localhost:5001/users/confirm?email=" + 
                    GenerateConfirmationString(user.Email)
                });

            return response;
        }

        private string GenerateConfirmationString(string mail)
        {
            return _encryptionService.EncryptString(mail);
        }

        public async Task<bool> ConfirmUserMail(string encryptedEmail)
        {
            encryptedEmail = encryptedEmail.Replace(' ', '+');

            var userEmail = _encryptionService.DecryptString(encryptedEmail);

            var user = await _genericClientRepository.GetByPredicate(x => x.Email == userEmail);

            if(user != null)
            {
                user.IsConfirmed = true;
                await _genericClientRepository.Update(user);
            }

            return user != null;
        }

        private User Map(UserDto userDto)
        {
            return new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Password = userDto.Password,
                Email = userDto.Email,
                BirthDate = userDto.BirthDate
            };
        }
    }
}
