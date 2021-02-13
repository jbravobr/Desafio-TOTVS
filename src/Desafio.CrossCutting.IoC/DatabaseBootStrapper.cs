using Desafio.Infra.Repositories.Context;
using Desafio.Infra.Repositories.Contracts.Base;
using Desafio.Infra.Repositories.Contracts.Writes;
using Desafio.Infra.Repositories.Implementations;
using Desafio.Infra.Repositories.Implementations.Base;
using Desafio.Infra.Repositories.Logs.Contracts;
using Desafio.Infra.Repositories.Logs.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SimpleInjector;

namespace Desafio.CrossCutting.IoC
{
    public static class DatabaseBootStrapper
    {
        public static void RegisterServices(Container container, string connection)
        {
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Transient);
            container.Register<IPdvHistoryRepository, PdvHistoryRepository>(Lifestyle.Transient);

            container.Register<IDesignTimeDbContextFactory<DatabaseContext>, DbContextFactory>(Lifestyle.Scoped);
            container.Register<DatabaseContext>(() =>
            {
                var builder = new DbContextOptionsBuilder<DatabaseContext>().UseSqlServer(connection);
                return new DatabaseContext(builder.Options);
            }, Lifestyle.Scoped);

            container.Register<IGatewayLogRepository, GatewayLogRepository>(Lifestyle.Transient);
        }
    }
}