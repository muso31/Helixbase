using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using System;

namespace Helixbase.Foundation.DI
{
    public class RegisterControllers : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMvcControllers(
                "Helixbase.Feature.*");
        }
    }
}