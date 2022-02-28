
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OpenPersonalBudget.API.Data;
using OpenPersonalBudget.API.Helpers;
using OpenPersonalBudget.API.Interfaces;
using OpenPersonalBudget.API.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OpenPersonalBudget.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;   
            _configuration = configuration;
        }

        public async Task<string> AuthenticateUser(string username, string password)
        {
            var user = (await _unitOfWork.UserRepository.GetAll())
                .ToList()
                .FirstOrDefault(u => u.Username == username && u.Password == PasswordHelper.Hash(password));

            if (user == null) return string.Empty;

            var token = BuildToken(user);

            return token;
        }

        public string BuildToken(UserModel user)
        {
            var secretKey = _configuration.GetSection("Jwt:Key").Value;
            var keyAsBytes = Encoding.ASCII.GetBytes(secretKey);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyAsBytes), SecurityAlgorithms.HmacSha256)
            };

            var handler = new JwtSecurityTokenHandler();
            var newTokenCreated = handler.CreateToken(descriptor);


            return handler.WriteToken(newTokenCreated);
        }
    }
}
