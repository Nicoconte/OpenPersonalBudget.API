using OpenPersonalBudget.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenPersonalBudget.API.Data.Repositories.Interfaces
{
    public interface IOperationRepository
    {
        Task<bool> Insert(OperationModel operation);
        Task<OperationModel> Update(OperationModel operation);
        Task<OperationModel> GetById(object id);
        Task<IEnumerable<OperationModel>> GetAll();
        Task<bool> Delete(object id);
    }
}
