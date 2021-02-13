using Desafio.Infra.Repositories.Contracts;
using Desafio.Infra.Repositories.Contracts.Base;
using Desafio.Infra.Repositories.Implementations;
using SimpleInjector;

namespace Desafio.CrossCutting.IoC
{
    public static class DatabaseBootStrapper
    {
        public static void RegisterServices(Container container)
        {
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Transient);
            container.Register(typeof(IWriteRepository<>), typeof(IWriteRepository<>).Assembly, Lifestyle.Transient);
        }
    }
}