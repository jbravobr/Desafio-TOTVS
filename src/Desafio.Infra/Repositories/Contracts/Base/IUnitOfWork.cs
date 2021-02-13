using Desafio.Infra.Repositories.Contracts.Writes;

namespace Desafio.Infra.Repositories.Contracts.Base
{
    public interface IUnitOfWork
    {
        IPdvRepository PdvRepository { get; }
        IUserRepository UserRepository { get; }
        IPdvHistoryRepository PdvHistoryRepository { get; }

        void Commit();
    }
}