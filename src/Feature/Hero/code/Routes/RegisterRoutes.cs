using Sitecore.Pipelines;
using System.Web.Mvc;
using System.Web.Routing;

namespace Helixbase.Feature.Hero.Routes
{
    public class RegisterRoutes
    {
        /// <summary>
        /// This route is NOT required for the Hero module, it is only here as an example of how to register a route
        /// </summary>
        /// <param name="args"></param>
        public void Process(PipelineArgs args)
        {
            RouteTable.Routes.MapRoute("Feature.Hero", "HeroPath/Hero",
              new { controller = "Hero", action = "Hero" });
        }
    }
}