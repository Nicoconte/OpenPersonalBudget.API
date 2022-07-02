using OpenPersonalBudget.API.Data.Repositories;
using OpenPersonalBudget.API.Data.Repositories.Interfaces;
using System;

namespace OpenPersonalBudget.API.Data.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IAppMsgRepository AppMsgRepository { get; }
        public IAccountBalanceRepository AccountBalanceRepository { get; }
        public IOperationRepository OperationRepository { get; }

        public UnitOfWork(
            IUserRepository userRepository,
            IAppMsgRepository appMsgRepository,
            IAccountBalanceRepository accountBalanceRepository,
            IOperationRepository operationRepository
        )
        {
            UserRepository = userRepository;
            AppMsgRepository = appMsgRepository;
            AccountBalanceRepository = accountBalanceRepository;
            OperationRepository = operationRepository;
        }
    }
}
