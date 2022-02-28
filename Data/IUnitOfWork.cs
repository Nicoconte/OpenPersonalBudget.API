using PersonalBudget.API.Data.Repositories;

namespace PersonalBudget.API.Data
{
    public interface IUnitOfWork
    {

        public IUserRepository UserRepository { get; }

        public IAppMsgRepository AppMsgRepository { get; }

        public void Commit();
    }
}
