using Autofac;
using System.Data;

namespace VictorKrogh.NET.Data.Extensions.Autofac.DependencyInjection;

public static class UnitOfWorkContainerBuilderExtensions
{
    public static ContainerBuilder RegisterUnitOfWork(this ContainerBuilder containerBuilder)
    {
        containerBuilder
            .RegisterType<UnitOfWorkFactory>()
            .As<IUnitOfWorkFactory>()
            .InstancePerLifetimeScope();

        containerBuilder.Register((cc, p) =>
        {
            var unitOfWorkFactory = cc.Resolve<IUnitOfWorkFactory>();

            if (p.Any())
            {
                return unitOfWorkFactory.Create(p.TypedAs<IsolationLevel>());
            }

            return unitOfWorkFactory.Create();
        })
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        return containerBuilder;
    }
}
