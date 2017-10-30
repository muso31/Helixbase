using Helixbase.Feature.Hero.Factories;
using Helixbase.Feature.Hero.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Helixbase.Feature.Hero.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IHeroFactory, HeroFactory>();
            serviceCollection.AddTransient<IHeroService, HeroService>();
        }
    }
}