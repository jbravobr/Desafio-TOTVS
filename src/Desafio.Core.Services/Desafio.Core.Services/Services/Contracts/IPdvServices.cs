using Desafio.Core.Domain.Models;
using System;
using System.Collections.Generic;

namespace Desafio.Core.Services.Contracts
{
    public interface IPdvServices
    {
        Pdv PayBill(double payment, double total);

        ICollection<PdvHistory> GetPdvHistories(DateTime startDate, DateTime endDate);
    }
}