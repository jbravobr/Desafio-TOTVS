using Desafio.Core.Domain.Models;

namespace Desafio.Infra.Repositories.Contracts.Base
{
    public interface IUnitOfWork
    {
        IPdvReadRepository PdvRepository { get; }
        IUserReadRepository UserRepository { get; }

        void Commit();
    }
}