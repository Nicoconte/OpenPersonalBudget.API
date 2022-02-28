using OpenPersonalBudget.API.Data.Repositories;
using OpenPersonalBudget.API.Data.Repositories.Interfaces;
using System;

namespace OpenPersonalBudget.API.Data.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IUserRepository UserRepository { get; }
        public IAppMsgRepository AppMsgRepository { get; }
        public IAccountBalanceRepository AccountBalanceRepository { get; }
        public IOperationRepository OperationRepository { get; }


        private DBContext _dbContext;
        private bool _disposed = false;

        public UnitOfWork(
            DBContext dbContext,
            IUserRepository userRepository,
            IAppMsgRepository appMsgRepository,
            IAccountBalanceRepository accountBalanceRepository,
            IOperationRepository operationRepository
        )
        {
            _dbContext = dbContext;
            UserRepository = userRepository;
            AppMsgRepository = appMsgRepository;
            AccountBalanceRepository = accountBalanceRepository;
            OperationRepository = operationRepository;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                return;

            if (disposing)
                _dbContext.Dispose();

            _disposed = true;
        }
    }
}
