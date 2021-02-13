using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infra.Repositories.Contracts
{
    public interface IPdvReadRepository
    {
        Task<object> PayBill(object payment);

        Task<object> GetPaymentsHistory(DateTime dateInit, DateTime dateEnd);
    }
}