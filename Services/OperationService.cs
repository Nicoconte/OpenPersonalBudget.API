using OpenPersonalBudget.API.Data.Repositories.Interfaces;
using OpenPersonalBudget.API.Interfaces;
using OpenPersonalBudget.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenPersonalBudget.API.Services
{
    public class OperationService : IOperationService
    {

        private readonly IUnitOfWork _unitOfWork;

        public OperationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationModel> CreateOperation(OperationModel operation)
        {
            if (operation == null) return null;

            var status = await _unitOfWork.OperationRepository.Insert(operation);

            if (status)
            {
                return operation;
            }

            return null;
        }

        public async Task<bool> DeleteOperation(object id)
        {
            var deleted = await _unitOfWork.OperationRepository.Delete(id);

            return deleted;
        }

        public async Task<bool> Exists(object id)
        {
            return (await _unitOfWork.OperationRepository.GetById(id)) != null;
        }

        public async Task<List<OperationModel>> GetAllOperations()
        {
            return (await _unitOfWork.OperationRepository.GetAll()).ToList();
        }

        public async Task<List<OperationModel>> GetAllUserOperations(UserModel user)
        {
            return (await _unitOfWork.OperationRepository.GetAll()).ToList().Where(x => x.User == user.Id).ToList();    
        }

        public async Task<OperationModel> GetOperation(object id)
        {
            return await _unitOfWork.OperationRepository.GetById(id);
        }

        public async Task<OperationModel> UpdateOperation(OperationModel operation)
        {
            if (operation == null) return null;

            var newOp = await _unitOfWork.OperationRepository.GetById(operation.Id);

            newOp.Amount = operation.Amount;
            newOp.Description = operation.Description;
            newOp.Category = operation.Category;
            newOp.PaymentType = operation.PaymentType;
            newOp.OperationType = operation.OperationType;

            var op = await _unitOfWork.OperationRepository.Update(newOp);

            if (op != null)
            {
                return op;
            }

            return null;
        }
    }
}
