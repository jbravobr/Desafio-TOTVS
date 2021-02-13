using Desafio.Core.Domain.Models;

namespace Desafio.Infra.Repositories.Contracts
{
    public interface IUserRepository
    {
        User CheckUserExists(string email, string password);
    }
}