using Desafio.Infra.Repositories.Contracts;
using Desafio.Infra.Repositories.Implementations.Base;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Desafio.Infra.Repositories.Implementations
{
    internal class UserReadRepository : ReadRepositoryBase, IUserReadRepository
    {
        public UserReadRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public Task<object> CheckUserExists(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}