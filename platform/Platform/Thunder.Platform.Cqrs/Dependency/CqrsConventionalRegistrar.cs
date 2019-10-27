using MediatR;
using Thunder.Platform.Core.Dependency;

namespace Thunder.Platform.Cqrs.Dependency
{
    public class CqrsConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.Services.AddMediatR(context.Assembly);
        }
    }
}
