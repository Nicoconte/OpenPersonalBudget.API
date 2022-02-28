using OpenPersonalBudget.API.Models;
using System.Threading.Tasks;

namespace OpenPersonalBudget.API.Interfaces
{
    public interface IAuthenticationService
    {
        public string BuildToken(UserModel user);

        public Task<string> AuthenticateUser(string username, string password);
    }
}
