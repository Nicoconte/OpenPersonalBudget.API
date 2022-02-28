using OpenPersonalBudget.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenPersonalBudget.API.Interfaces
{
    public interface IOperationService
    {
        public Task<OperationModel> CreateOperation(OperationModel operation);
        public Task<OperationModel> UpdateOperation(OperationModel operation);
        public Task<bool> DeleteOperation(object id);
        public Task<List<OperationModel>> GetAllOperations();
        public Task<OperationModel> GetOperation(object id);
        public Task<bool> Exists(object id);
    }
}
