using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infra.Repositories.Contracts
{
    public interface IUserReadRepository
    {
        Task<object> CheckUserExists(string email, string password);
    }
}