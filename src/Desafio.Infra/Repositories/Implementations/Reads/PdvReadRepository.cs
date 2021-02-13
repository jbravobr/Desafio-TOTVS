using Desafio.Infra.Repositories.Contracts;
using Desafio.Infra.Repositories.Implementations.Base;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Desafio.Infra.Repositories.Implementations
{
    internal class PdvReadRepository : ReadRepositoryBase, IPdvReadRepository
    {
        public PdvReadRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public Task<object> PayBill(object payment)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetPaymentsHistory(DateTime dateInit, DateTime dateEnd)
        {
            throw new NotImplementedException();
        }
    }
}