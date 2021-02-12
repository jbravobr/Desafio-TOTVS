using Desafio.Core.Domain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Core.Domain.Services.Implementations
{
    public class PdvServices : IPdvServices
    {
        private readonly object _pdvRepository;

        public PdvServices(object pdvRepository)
        {
            _pdvRepository = pdvRepository;
        }

        public async Task<object> PayBill(object payment)
        {
            throw new NotImplementedException();
        }
    }
}