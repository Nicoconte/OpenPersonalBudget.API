using OpenPersonalBudget.API.Data;
using OpenPersonalBudget.API.Data.Repositories.Interfaces;
using OpenPersonalBudget.API.Interfaces;
using OpenPersonalBudget.API.Models;
using System.Linq;
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

        public async Task<AccountBalanceModel> UpdateAmountFromUserAccount(UserModel user, OperationModel operation)
        {
            var account = (await _unitOfWork.AccountBalanceRepository.GetAll()).ToList().Find(a => a.User.Id == user.Id);

            if (account == null) return null;

            if (operation.OperationType == OperationTypeEnum.CashIncome)
                account.Amount += operation.Amount;
            else
                account.Amount -= operation.Amount;

            await _unitOfWork.AccountBalanceRepository.Update(account);

            _unitOfWork.Commit();

            return account;
        }
    }
}
