using Helixbase.Foundation.Repository.Models;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Helixbase.Foundation.Repository.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IContentRepository, SitecoreContentRepository>();
        }
    }
}