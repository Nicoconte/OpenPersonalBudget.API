using EasyCrudNET;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenPersonalBudget.API.Data.Repositories.Interfaces;
using OpenPersonalBudget.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenPersonalBudget.API.Data.Repositories.Implementations
{
    public class AccountBalanceRepository : IAccountBalanceRepository
    {

        private readonly EasyCrud _easyCrud;
        private readonly IConfiguration _configuration;

        public AccountBalanceRepository(EasyCrud easyCrud, IConfiguration configuration)
        {
            _easyCrud = easyCrud;
            _configuration = configuration;

            _easyCrud.SetSqlConnection(_configuration.GetSection("TestConnectionString").Value);
        }

        public async Task<bool> Insert(AccountBalanceModel account)
        {
            var saved = await _easyCrud
                .Insert()
                .Into("AccountBalances")
                .Values("@id", "@userId", "@amount", "@date")
                .ExecuteAsync(new
                {
                    id = account.Id,
                    userId = account.User,
                    amount = account.Amount,
                    date = DateTime.Now,
                });

            return saved != 0;
        }

        public async Task<bool> Delete(object id)
        {
            var deleted = await _easyCrud
                .Delete()
                .From("AccountBalances")
                .Where("Id", "@id").ExecuteAsync(new
                {
                    id = id
                });

            return deleted != 0;
        }

        public async Task<AccountBalanceModel> GetById(object id)
        {
            var account = await _easyCrud
                .Select("*")
                .From("AccountBalances")
                .Where("Id", "@id")
                .ExecuteAsync<AccountBalanceModel>(new
                {
                    id = id
                });

            return account.FirstOrDefault();
        }

        public async Task<AccountBalanceModel> Update(AccountBalanceModel account)
        {

            var updated = _easyCrud
                .Update("AccountBalances")
                .Set("Amount", "@amount")
                .Where("Id", "@id")
                .Execute(new
                {
                    id = account.Id,
                    amount = account.Amount,
                });


            AccountBalanceModel currentAccount = null;

            if (updated != 0)
            {
                currentAccount = (await _easyCrud
                    .Select("*")
                    .From("AccountBalances")
                    .Where("Id", "@id")
                    .ExecuteAsync<AccountBalanceModel>(new
                    {
                        id = account.Id
                    }))
                    .FirstOrDefault();
            }

            return currentAccount;
        }

        public async Task<IEnumerable<AccountBalanceModel>> GetAll()
        {
            return await _easyCrud.Select("*").From("AccountBalances").ExecuteAsync<AccountBalanceModel>();
        }
    }
}
