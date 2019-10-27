using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Thunder.Platform.Core.Dependency
{
    public class BasicConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.Services.Scan(scan => scan
                .FromAssemblies(context.Assembly)

                // Register transient instances.
                .AddClasses(@class => @class.AssignableTo<ITransientInstance>())
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()

                // Register singleton instances.
                .AddClasses(@class => @class.AssignableTo<ISingletonInstance>())
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime()

                // Register validator instances.
                .AddClasses(@class => @class.AssignableTo(typeof(IValidator<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
        }
    }
}
