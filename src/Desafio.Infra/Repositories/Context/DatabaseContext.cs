using Desafio.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Infra.Repositories.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<PdvHistory> PdvHistories { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<PdvHistory>().ToTable("PdvHistory");
        }
    }
}