using OpenPersonalBudget.API.Models;
using System.Threading.Tasks;

namespace OpenPersonalBudget.API.Interfaces
{
    public interface IUserService
    {
        public UserModel CreateUser(UserModel user);
        public Task<bool> VerifyIfUserExists(string username);
    }
}
 