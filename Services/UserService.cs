using PersonalBudget.API.Data;
using PersonalBudget.API.Helpers;
using PersonalBudget.API.Interfaces;
using PersonalBudget.API.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBudget.API.Services
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }


        public UserModel CreateUser(UserModel user)
        {

            if (user == null) return null;

            user.Password = PasswordHelper.Hash(user.Password);

            _unitOfWork.UserRepository.Insert(user);

            _unitOfWork.Commit();

            return user;
        }

        public async Task<bool> VerifyIfUserExists(string username)
        {
            return (await _unitOfWork.UserRepository.GetAll()).ToList().Exists(p => p.Username == username); 
        }
    }
}
