using OpenPersonalBudget.API.Models;
using System.Threading.Tasks;

namespace OpenPersonalBudget.API.Interfaces
{
    public interface IAccountBalanceService
    {
        public Task<AccountBalanceModel> CreateAccountBalance(AccountBalanceModel account);
        public Task<AccountBalanceModel> UpdateAmountFromUserAccount(UserModel user, OperationModel operation);
    }
}
