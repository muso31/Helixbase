using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Pipelines;

namespace Helixbase.Feature.Hero.Routes
{
    public class RegisterRoutes
    {
        /// <summary>
        ///     This route is NOT required for the Hero feature neither will it work, it is only here as an example of how to
        ///     register a route. We can also use Sitecore native https://demo.helixbase/api/sitecore/hero/someAction
        /// </summary>
        /// <param name="args"></param>
        public void Process(PipelineArgs args)
        {
            RouteTable.Routes.MapRoute("Feature.Hero", "HeroPath/Hero",
                new {controller = "Hero", action = "SomeAction"});
        }
    }
}