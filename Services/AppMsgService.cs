using OpenPersonalBudget.API.Data;
using OpenPersonalBudget.API.Interfaces;

namespace OpenPersonalBudget.API.Services
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
