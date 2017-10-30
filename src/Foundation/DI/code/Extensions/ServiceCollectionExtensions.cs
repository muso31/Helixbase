using Microsoft.Extensions.DependencyInjection;
using Sitecore.Mvc.Controllers;
using System;
using System.Linq;
using System.Reflection;
using Helixbase.Foundation.Tools.Reflection;

namespace Helixbase.Foundation.DI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMvcControllers(this IServiceCollection serviceCollection, params string[] assemblyFilters)
        {
            var assemblies = GetAssemblies.GetByFilter(assemblyFilters);

            AddMvcControllers(serviceCollection, assemblies);
        }

        public static void AddMvcControllers(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        {
            var controllers = GetTypes.GetTypesImplementing<SitecoreController>(assemblies)
                .Where(controller => controller.Name.EndsWith("Controller", StringComparison.Ordinal));

            foreach (var controller in controllers)
            {
                serviceCollection.AddTransient(controller);
            }
        }
    }
}