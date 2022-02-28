using Microsoft.EntityFrameworkCore;
using OpenPersonalBudget.API.Data.Repositories.Interfaces;
using OpenPersonalBudget.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenPersonalBudget.API.Data.Repositories.Implementations
{
    public class AccountBalanceRepository : IAccountBalanceRepository
    {

        private readonly DBContext _dbContext;

        public AccountBalanceRepository(DBContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public async Task<bool> Insert(AccountBalanceModel account)
        {
            var saved = await _dbContext.AccountBalances.AddAsync(account);

            return saved != null;
        }

        public async Task<bool> Delete(object id)
        {
            AccountBalanceModel account = await _dbContext.AccountBalances.FindAsync(id);

            var deleted = _dbContext.Remove(account);

            return deleted.State.HasFlag(deleted.State);
        }

        public async Task<AccountBalanceModel> GetById(object id)
        {
            var account = await _dbContext.AccountBalances.FindAsync(id);

            return account;
        }

        public async Task<AccountBalanceModel> Update(AccountBalanceModel account)
        {
            var currentAccount = await _dbContext.AccountBalances.FindAsync(account.Id);

            currentAccount = account;

            _dbContext.AccountBalances.Update(currentAccount);

            return currentAccount;
        }

        public async Task<IEnumerable<AccountBalanceModel>> GetAll()
        {
            return await _dbContext.AccountBalances.ToListAsync();
        }
    }
}
