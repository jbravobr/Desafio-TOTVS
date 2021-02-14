using Desafio.Core.Domain.Models;
using System;
using System.Collections.Generic;

namespace Desafio.Infra.Repositories.Logs.Contracts
{
    public interface IGatewayLogRepository
    {
        Log Create(Log log);

        List<Log> Get();

        Log Get(Guid id);
    }
}