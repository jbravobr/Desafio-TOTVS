using Desafio.Core.Domain.Models;
using Desafio.Core.Services.Services.Contracts;
using Desafio.Infra.Repositories.Logs.Contracts;
using System;
using System.Collections.Generic;

namespace Desafio.Core.Services.Services.Implementations
{
    public class GatewayLogServices : IGatewayLogServices
    {
        private readonly IGatewayLogRepository _gatewayLogRepository;

        public GatewayLogServices(IGatewayLogRepository gatewayLogRepository)
        {
            _gatewayLogRepository = gatewayLogRepository;
        }

        public Log Create(Log log)
        {
            return _gatewayLogRepository.Create(log);
        }

        public List<Log> Get()
        {
            return _gatewayLogRepository.Get();
        }

        public Log Get(Guid id)
        {
            return _gatewayLogRepository.Get(id);
        }
    }
}