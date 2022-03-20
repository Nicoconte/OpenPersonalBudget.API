using OpenPersonalBudget.API.Models;
using System.Threading.Tasks;

namespace OpenPersonalBudget.API.Interfaces
{
    public interface IAuthenticationService
    {
        private string _BuildToken(UserModel user);

        public Task<string> AuthenticateUser(string username, string password);
    }
}
