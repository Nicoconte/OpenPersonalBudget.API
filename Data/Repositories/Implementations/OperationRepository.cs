using Microsoft.EntityFrameworkCore;
using OpenPersonalBudget.API.Data.Repositories.Interfaces;
using OpenPersonalBudget.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenPersonalBudget.API.Data.Repositories.Implementations
{
    public class OperationRepository : IOperationRepository
    {

        private readonly DBContext _dbContext;

        public OperationRepository(DBContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public async Task<bool> Insert(OperationModel operation)
        {
            var saved = await _dbContext.Operations.AddAsync(operation);

            return saved != null;
        }

        public async Task<bool> Delete(object id)
        {
            OperationModel operation = await _dbContext.Operations.FindAsync(id);

            var deleted = _dbContext.Remove(operation);

            return deleted.State.HasFlag(deleted.State);
        }

        public async Task<OperationModel> GetById(object id)
        {
            var operation = await _dbContext.Operations.FindAsync(id);

            return operation;
        }

        public async Task<OperationModel> Update(OperationModel operation)
        {
            var currentOp = await _dbContext.Operations.FindAsync(operation.Id);

            currentOp = operation;

            _dbContext.Operations.Update(currentOp);

            return currentOp;
        }

        public async Task<IEnumerable<OperationModel>> GetAll()
        {
            return await _dbContext.Operations.ToListAsync();
        }

    }
}
