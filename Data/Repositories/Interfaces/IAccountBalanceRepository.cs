using OpenPersonalBudget.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenPersonalBudget.API.Data.Repositories.Interfaces
{
    public interface IAccountBalanceRepository
    {
        Task<bool> Insert(AccountBalanceModel account);
        Task<AccountBalanceModel> Update(AccountBalanceModel account);
        Task<AccountBalanceModel> GetById(object id);
        Task<IEnumerable<AccountBalanceModel>> GetAll();
        Task<bool> Delete(object id);
    }
}
