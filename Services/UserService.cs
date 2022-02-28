using OpenPersonalBudget.API.Data;
using OpenPersonalBudget.API.Data.Repositories.Interfaces;
using OpenPersonalBudget.API.Helpers;
using OpenPersonalBudget.API.Interfaces;
using OpenPersonalBudget.API.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace OpenPersonalBudget.API.Services
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

        public async Task<UserModel> GetUser(object id)
        {
            return await _unitOfWork.UserRepository.GetById(id);
        }

        public async Task<UserModel> GetUserFromClaims(ClaimsPrincipal userClaims)
        {
            var id = (userClaims.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;

            return await _unitOfWork.UserRepository.GetById(id);
        }

        public async Task<bool> VerifyIfUserExists(string username)
        {
            return (await _unitOfWork.UserRepository.GetAll()).ToList().Exists(p => p.Username == username); 
        }
    }
}
