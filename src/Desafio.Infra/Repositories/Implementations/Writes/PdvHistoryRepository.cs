using Dapper;
using Desafio.Core.Domain.Models;
using Desafio.Infra.Repositories.Context;
using Desafio.Infra.Repositories.Contracts.Writes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Desafio.Infra.Repositories.Implementations.Base
{
    public class PdvHistoryRepository : IPdvHistoryRepository
    {
        protected readonly DatabaseContext context;
        private DbSet<PdvHistory> entities;

        public PdvHistoryRepository(DatabaseContext context)
        {
            this.context = context;
            entities = context.Set<PdvHistory>();
        }

        public void Insert(PdvHistory entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(PdvHistory entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            if (id == null) throw new ArgumentNullException("entity");

            PdvHistory entity = entities.SingleOrDefault(s => s.Id == id);
            entities.Remove(entity);
            context.SaveChanges();
        }

        public ICollection<PdvHistory> GetPaymentsHistory(DateTime startDate, DateTime endDate)
        {
            var sql = $"SELECT * FROM [PdvHistory] WHERE CreatedAt >= '{startDate.ToString("yyyy-MM-dd HH:mm:ss")}' AND CreatedAt <= '{endDate.ToString("yyyy-MM-dd HH:mm:ss")}'";
            return context.Database.GetDbConnection().Query<PdvHistory>(
                sql
            ).ToList();
        }
    }
}