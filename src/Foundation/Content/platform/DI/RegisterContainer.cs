using Helixbase.Foundation.Content.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using System;

namespace Helixbase.Foundation.Content.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IContentRepository, ContentRepository>();

            // Allow IContentRepository to be resolved on-demand by singletons
            serviceCollection.AddSingleton<Func<IContentRepository>>(_ => () => ServiceLocator.ServiceProvider.GetService<IContentRepository>());

            serviceCollection.AddTransient<IRenderingRepository, RenderingRepository>();

            serviceCollection.AddTransient<IContextRepository, ContextRepository>();

            // Allow IContextRepository to be resolved on-demand by singletons
            serviceCollection.AddSingleton<Func<IContextRepository>>(_ => () => ServiceLocator.ServiceProvider.GetService<IContextRepository>());
        }
    }
}
