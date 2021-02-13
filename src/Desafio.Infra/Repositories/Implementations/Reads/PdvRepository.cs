using Dapper;
using Desafio.Core.Domain.Models;
using Desafio.Infra.Repositories.Contracts;
using Desafio.Infra.Repositories.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Desafio.Infra.Repositories.Implementations.Reads
{
    internal class PdvRepository : ReadRepositoryBase, IPdvRepository
    {
        public PdvRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public ICollection<PdvHistory> GetPaymentsHistory(DateTime startDate, DateTime endDate)
        {
            return Connection.Query<PdvHistory>(
                $"SELECT * FROM PdvHistory WHERE CreatedAt >= {startDate.ToString("yyyy-MM-dd")} AND CreateAt <= {endDate.ToString("yyyy-MM-dd")}",
                transaction: Transaction
            ).ToList();
        }
    }
}