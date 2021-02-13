using Desafio.Core.Domain.Models;
using Desafio.Infra.Repositories.Context;
using Desafio.Infra.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Desafio.Infra.Repositories.Implementations.Writes
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        protected readonly DatabaseContext context;
        private DbSet<T> entities;

        public WriteRepository(DatabaseContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public void Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            if (id == null) throw new ArgumentNullException("entity");

            T entity = entities.SingleOrDefault(s => s.Id == id);
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}