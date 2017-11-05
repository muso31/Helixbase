using Helixbase.Foundation.Content.Repositories;
using Helixbase.Foundation.Content.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Helixbase.Foundation.Content.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IContentRepository, ContentRepository>();
            serviceCollection.AddTransient<IRenderingRepository, RenderingRepository>();
            serviceCollection.AddTransient<ICmsInfoRepository, CmsInfoRepository>();
            serviceCollection.AddTransient<IMediatorService, MediatorService>();
        }
    }
}