using PersonalBudget.API.Models;
using System.Threading.Tasks;

namespace PersonalBudget.API.Interfaces
{
    public interface IUserService
    {
        public UserModel CreateUser(UserModel user);
        public Task<bool> VerifyIfUserExists(string username);
    }
}
 