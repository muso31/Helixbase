using Helixbase.Feature.Hero.Controllers;
using Helixbase.Feature.Hero.Service;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Helixbase.Feature.Hero.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<HeroController>();
            serviceCollection.AddTransient<IHeroService, HeroService>();
        }
    }
}