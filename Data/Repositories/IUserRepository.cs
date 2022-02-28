using PersonalBudget.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalBudget.API.Data.Repositories
{
    public interface IUserRepository
    {
        Task<bool> Insert(UserModel user);
        Task<UserModel> Update(UserModel user);
        Task<UserModel> GetById(object id);
        Task<IEnumerable<UserModel>> GetAll();
        Task<bool> Delete(object id);
        Task<bool> Exists(UserModel user);
    }
}
