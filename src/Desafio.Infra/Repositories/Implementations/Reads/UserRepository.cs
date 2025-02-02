﻿using Dapper;
using Desafio.Core.Domain.Models;
using Desafio.Infra.Repositories.Contracts;
using Desafio.Infra.Repositories.Implementations.Base;
using System.Data;
using System.Linq;

namespace Desafio.Infra.Repositories.Implementations.Reads
{
    internal class UserRepository : ReadRepositoryBase, IUserRepository
    {
        public UserRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public User CheckUserExists(string email, string password)
        {
            var sql = $"SELECT * FROM [User] WHERE Email = '{email}' AND Password = '{password}'";
            return Connection.Query<User>(
                sql,
                transaction: Transaction
            ).FirstOrDefault();
        }
    }
}