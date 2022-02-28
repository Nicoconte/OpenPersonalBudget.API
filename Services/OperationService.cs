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
                _unitOfWork.Commit();

                return operation;
            }

            return null;
        }

        public async Task<bool> DeleteOperation(object id)
        {
            return await _unitOfWork.OperationRepository.Delete(id);            
        }

        public async Task<bool> Exists(object id)
        {
            return (await _unitOfWork.OperationRepository.GetById(id)) != null;
        }

        public async Task<List<OperationModel>> GetAllOperations()
        {
            return (await _unitOfWork.OperationRepository.GetAll()).ToList();
        }

        public async Task<OperationModel> GetOperation(object id)
        {
            return await _unitOfWork.OperationRepository.GetById(id);
        }

        public async Task<OperationModel> UpdateOperation(OperationModel operation)
        {
            if (operation == null) return null;

            var op = await _unitOfWork.OperationRepository.Update(operation);

            if (op != null)
            {
                _unitOfWork.Commit();

                return op;
            }

            return null;
        }
    }
}
