using OpenPersonalBudget.API.Data.Repositories;
using System;

namespace OpenPersonalBudget.API.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IUserRepository UserRepository { get; }

        public IAppMsgRepository AppMsgRepository { get; }

        private DBContext _dbContext;
        private bool _disposed = false;

        public UnitOfWork(
            DBContext dbContext,
            IUserRepository userRepository,
            IAppMsgRepository appMsgRepository
        )
        {
            _dbContext = dbContext;
            UserRepository = userRepository;
            AppMsgRepository = appMsgRepository;
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
