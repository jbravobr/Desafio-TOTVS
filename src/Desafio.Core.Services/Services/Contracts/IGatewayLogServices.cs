using Desafio.Core.Domain.Models;
using System;
using System.Collections.Generic;

namespace Desafio.Core.Services.Services.Contracts
{
    public interface IGatewayLogServices
    {
        Log Create(Log log);

        List<Log> Get();

        Log Get(Guid id);
    }
}