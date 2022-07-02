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

    public class UserRepository : IUserRepository
    {
        private readonly EasyCrud _easyCrud;

        public UserRepository(EasyCrud easyCrud)
        {
            _easyCrud = easyCrud;
        }

        public async Task<bool> Insert(UserModel user)
        {
            var saved = await _easyCrud
                .Insert()
                .Into("Users")
                .Values("@id", "@username", "@email", "@password", "@date")
                .ExecuteAsync(new
                {
                    id = user.Id,
                    username = user.Username,
                    password = user.Password,
                    email  = user.Email,
                    date = user.CreatedAt,
                });

            return saved != 0;
        }

        public async Task<bool> Delete(object id)
        {
            var deleted = await _easyCrud
                .Delete()
                .From("Users")
                .Where("Id", "@id").ExecuteAsync(new
                {
                    id = id
                });

            return deleted != 0;
        }

        public async Task<UserModel> GetById(object id)
        {
            var user = await _easyCrud
                .Select("*")
                .From("Users")
                .Where("Id", "@id")
                .ExecuteAsync<UserModel>(new
                {
                    id = id
                });

            return user.FirstOrDefault();
        }

        public async Task<UserModel> Update(UserModel user)
        {
            var updated = _easyCrud
                .Update("Users")
                .Set("Username", "@username")
                .Set("Email", "@email")
                .Where("Id", "@id")
                .Execute(new
                {
                    id = user.Id,
                    username = user.Username,
                    email = user.Email
                });


            UserModel currentUser = null;

            if (updated != 0)
            {
                currentUser = (await _easyCrud
                    .Select("*")
                    .From("AccountBalances")
                    .Where("Id", "@id")
                    .ExecuteAsync<UserModel>(new
                    {
                        id = user.Id
                    }))
                    .FirstOrDefault();
            }

            return currentUser;
        }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            return await _easyCrud.Select("*").From("Users").ExecuteAsync<UserModel>();
        }

        public async Task<bool> Exists(UserModel user)
        {
            return (await _easyCrud
                .Select("TOP (1) *")
                .From("Users")
                .Where("Username", "@username")
                .And("Password", "@password")
                .ExecuteAsync<UserModel>(new
                {
                    username = user.Username,
                    password = user.Password
                }))
                .FirstOrDefault() != null;
        }
    }
}
