using Desafio.Core.Services.Contracts;
using Desafio.Core.Services.Implementations;
using Desafio.Core.Services.Services.Contracts;
using Desafio.Core.Services.Services.Implementations;
using SimpleInjector;

namespace Desafio.CrossCutting.IoC
{
    public static class SimpleInjectorBootStrapper
    {
        public static void RegisterServices(Container container)
        {
            container.Register<IUserServices, UserServices>(Lifestyle.Transient);
            container.Register<IPdvServices, PdvServices>(Lifestyle.Transient);
            container.Register<IGatewayLogServices, GatewayLogServices>(Lifestyle.Transient);
        }
    }
}