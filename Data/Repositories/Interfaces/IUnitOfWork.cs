using OpenPersonalBudget.API.Data.Repositories;

namespace OpenPersonalBudget.API.Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {

        public IUserRepository UserRepository { get; }
        public IAppMsgRepository AppMsgRepository { get; }
        public IAccountBalanceRepository AccountBalanceRepository { get; }
        public IOperationRepository OperationRepository { get; }
    }
}
