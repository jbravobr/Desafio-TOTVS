using Desafio.Core.Domain.Models;
using System;

namespace Desafio.Infra.Repositories.Contracts
{
    public interface IWriteRepository<T> where T : BaseEntity
    {
        void Insert(T entity);

        void Update(T entity);

        void Delete(Guid id);
    }
}