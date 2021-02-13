using Desafio.Infra.Repositories.Context;
using Desafio.Infra.Repositories.Contracts.Base;
using Desafio.Infra.Repositories.Contracts.Writes;
using Desafio.Infra.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;

namespace Desafio.CrossCutting.IoC
{
    public static class DatabaseBootStrapper
    {
        public static void RegisterServices(Container container, string connection)
        {
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Transient);
            container.Register<IPdvHistoryRepository, IPdvHistoryRepository>(Lifestyle.Transient);

            container.Register<DatabaseContext>(() =>
            {
                var builder = new DbContextOptionsBuilder<DatabaseContext>().UseSqlServer(connection);
                return new DatabaseContext(builder.Options);
            }, Lifestyle.Transient);
        }
    }
}