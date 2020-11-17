using Helixbase.Feature.Hero.Services;
using System.Diagnostics;
using Sitecore.LayoutService.Configuration;
using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
using Sitecore.Mvc.Presentation;

namespace Helixbase.Feature.Hero.LayoutService
{
    public class HeroContentResolver : RenderingContentsResolver
    {
        protected readonly IHeroBuilder HeroBuilder;

        public HeroContentResolver(IHeroBuilder heroBuilder)
        {
            Debug.Assert(heroBuilder != null);
            HeroBuilder = heroBuilder;
        }

        public override object ResolveContents(Rendering rendering, IRenderingConfiguration renderingConfig)
        {
            var heroResolverModel = HeroBuilder.GetHeroModel(this.GetContextItem(rendering, renderingConfig));
            return heroResolverModel;
        }
    }
}