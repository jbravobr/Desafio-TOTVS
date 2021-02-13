using Desafio.Core.Domain.Models;

namespace Desafio.Core.Services.Contracts
{
    public interface IUserServices
    {
        User CheckUserExists(string email, string password);
    }
}