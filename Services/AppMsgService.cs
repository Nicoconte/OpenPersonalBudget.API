using PersonalBudget.API.Data;
using PersonalBudget.API.Interfaces;

namespace PersonalBudget.API.Services
{
    public class AppMsgService : IAppMsgService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppMsgService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetMessage(string id)
        {
            return _unitOfWork.AppMsgRepository.Get(id);
        }
    }
}
