using OpenPersonalBudget.API.Models;
using System.Threading.Tasks;

namespace OpenPersonalBudget.API.Interfaces
{
    public interface IAccountBalanceService
    {
        public AccountBalanceModel CreateAccountBalance(AccountBalanceModel account);
        public Task<AccountBalanceModel> UpdateBalanceFromAccount(object id, float money, string operationType);
        //public AccountBalanceModel UpdateBalanceFromAccount(object id, OperationModel operation);
    }
}
