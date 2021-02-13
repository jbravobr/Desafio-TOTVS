using System.Data;

namespace Desafio.Infra.Repositories.Implementations.Base
{
    internal abstract class ReadRepositoryBase
    {
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { get { return Transaction.Connection; } }

        public ReadRepositoryBase(IDbTransaction transaction)
        {
            Transaction = transaction;
        }
    }
}