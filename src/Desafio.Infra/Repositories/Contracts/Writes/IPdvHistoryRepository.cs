using Desafio.Core.Domain.Models;
using System;
using System.Collections.Generic;

namespace Desafio.Infra.Repositories.Contracts.Writes
{
    public interface IPdvHistoryRepository
    {
        void Insert(PdvHistory pdvHistory);

        void Update(PdvHistory pdvHistory);

        void Delete(Guid id);

        ICollection<PdvHistory> GetPaymentsHistory(DateTime startDate, DateTime endDate);
    }
}