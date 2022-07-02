using OpenPersonalBudget.API.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OpenPersonalBudget.API.Interfaces
{
    public interface IUserService
    {
        public Task<UserModel> CreateUser(UserModel user);
        public Task<bool> VerifyIfUserExists(string username);
        public Task<UserModel> GetUser(object id);
        public Task<UserModel> GetUserFromClaims(ClaimsPrincipal userClaims);
    }
}
 