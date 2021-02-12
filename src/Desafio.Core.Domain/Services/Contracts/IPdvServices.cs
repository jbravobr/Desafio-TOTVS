using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Core.Domain.Services.Contracts
{
    public interface IPdvServices
    {
        Task<object> PayBill(object payment);
    }
}