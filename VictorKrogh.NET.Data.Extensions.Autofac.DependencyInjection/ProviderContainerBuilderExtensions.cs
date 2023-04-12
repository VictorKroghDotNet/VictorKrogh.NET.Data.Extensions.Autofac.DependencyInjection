using Autofac;
using System.Data;
using VictorKrogh.NET.Data.Provider;

namespace VictorKrogh.NET.Data.Extensions.Autofac.DependencyInjection;

public static class ProviderContainerBuilderExtensions
{
    public static void RegisterProvider<TProvider>(this ContainerBuilder containerBuilder)
    where TProvider : class, IProvider
    {
        containerBuilder.Register((cc, p) =>
        {
            var providerFactory = cc.Resolve<IProviderFactory>();

            if (p.Any())
            {
                return providerFactory.CreateProvider<TProvider>(p.TypedAs<IsolationLevel>());
            }

            return providerFactory.CreateProvider<TProvider>();
        })
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}