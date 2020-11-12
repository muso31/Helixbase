using Helixbase.Feature.Hero.Rendering.ViewModels;
using Sitecore.AspNet.RenderingEngine.Configuration;
using Sitecore.AspNet.RenderingEngine.Extensions;

namespace Helixbase.Feature.Hero.Rendering.Extensions
{
    public static class RenderingEngineOptionsExtensions
    {
        public static RenderingEngineOptions AddFeatureHeroContent(this RenderingEngineOptions options)
        {
            options
                .AddModelBoundView<HeroViewModel>("Hero");
            return options;
        }
    }
}