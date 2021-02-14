using Desafio.Core.Domain.Models;
using Desafio.Infra.Repositories.Logs.Contracts;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Desafio.Infra.Repositories.Logs.Implementations
{
    public class GatewayLogRepository : IGatewayLogRepository
    {
        private readonly IMongoCollection<Log> _logs;

        public GatewayLogRepository(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("LogDatabaseSettings:ConnectionString").Value);
            var database = client.GetDatabase(configuration.GetSection("LogDatabaseSettings:DatabaseName").Value);

            _logs = database.GetCollection<Log>(configuration.GetSection("LogDatabaseSettings:CollectionName").Value);
        }

        public List<Log> Get() =>
            _logs.Find(book => true).ToList();

        public Log Get(Guid id) =>
            _logs.Find<Log>(l => l.Id == id).FirstOrDefault();

        public Log Create(Log log)
        {
            _logs.InsertOne(log);
            return log;
        }
    }
}