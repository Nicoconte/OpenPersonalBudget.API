using Microsoft.EntityFrameworkCore;
using OpenPersonalBudget.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenPersonalBudget.API.Data.Repositories
{

    public class UserRepository : IUserRepository
    {
        private DBContext _dbContext;

        public UserRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Insert(UserModel user)
        {
            var saved = await _dbContext.Users.AddAsync(user);

            return saved != null;
        }

        public async Task<bool> Delete(object id)
        {
            UserModel user = await _dbContext.Users.FindAsync(id);

            var deleted = _dbContext.Remove(user);

            return deleted.State.HasFlag(deleted.State);
        }

        public async Task<UserModel> GetById(object id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            return user;
        }

        public async Task<UserModel> Update(UserModel user)
        {
            var currentUser = await _dbContext.Users.FindAsync(user.Id);

            currentUser = user;

            _dbContext.Users.Update(currentUser);

            return currentUser;
        }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<bool> Exists(UserModel user)
        {
            return (await _dbContext.Users.ToListAsync()).Exists(u => u.Username == user.Username);
        }
    }
}
