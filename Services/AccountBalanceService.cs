using OpenPersonalBudget.API.Data;
using OpenPersonalBudget.API.Interfaces;
using OpenPersonalBudget.API.Models;
using System.Threading.Tasks;

namespace OpenPersonalBudget.API.Services
{
    public class AccountBalanceService : IAccountBalanceService
    {

        private readonly IUnitOfWork _unitOfWork;

        public AccountBalanceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AccountBalanceModel CreateAccountBalance(AccountBalanceModel account)
        {

            if (account == null) return null;

            _unitOfWork.AccountBalanceRepository.Insert(account);

            _unitOfWork.Commit();

            return account;
        }

        public async Task<AccountBalanceModel> UpdateBalanceFromAccount(object id, float amount, string operationType)
        {
            var account = await _unitOfWork.AccountBalanceRepository.GetById(id);
            
            if (operationType == "MoneyIncome")
                account.Amount += amount;
            else
                account.Amount -= amount;
            
            await _unitOfWork.AccountBalanceRepository.Update(account);

            return account;
        }
    }
}
