using Autofac;
using System.Reflection;
using VictorKrogh.NET.Data.Provider;
using VictorKrogh.NET.Data.Repository;

namespace VictorKrogh.NET.Data.Extensions.Autofac.DependencyInjection;

public static class RepositoryContainerBuilderExtensions
{
    public static ContainerBuilder RegisterRepositories<TProvider>(this ContainerBuilder containerBuilder)
        where TProvider : class, IProvider
    {
        containerBuilder.RegisterRepositories(typeof(TProvider).Assembly);

        return containerBuilder;
    }

    public static ContainerBuilder RegisterRepositories(this ContainerBuilder containerBuilder, Assembly assembly)
    {
        containerBuilder.RegisterAssemblyTypes(assembly)
            .AssignableTo<IRepository>()
            .AsImplementedInterfaces();

        return containerBuilder;
    }
}
