using System.Threading.Tasks;

namespace Desafio.Core.Domain.Services.Contracts
{
    public interface IUserServices
    {
        Task<object> CheckUserExists(string email, string password);
    }
}