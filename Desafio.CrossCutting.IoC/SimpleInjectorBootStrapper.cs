using Desafio.Core.Domain.Services.Contracts;
using Desafio.Core.Domain.Services.Implementations;
using SimpleInjector;

namespace Desafio.CrossCutting.IoC
{
    public static class SimpleInjectorBootStrapper
    {
        public static void RegisterServices(Container container)
        {
            container.Register<IUserServices, UserServices>(Lifestyle.Transient);
            container.Register<IPdvServices, PdvServices>(Lifestyle.Transient);
        }
    }
}