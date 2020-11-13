using System.Web;
using Glass.Mapper.Sc;
using Helixbase.Feature.Hero.Models;
using Helixbase.Feature.Hero.ResolverModels;

namespace Helixbase.Feature.Hero.Factories
{
    public class HeroViewModelFactory : IHeroViewModelFactory
    {
        private readonly IGlassHtml _glassHtml;

        public HeroViewModelFactory(IGlassHtml glassHtml)
        {
            _glassHtml = glassHtml;
        }

        public HeroResolverModel CreateHeroViewModel(IHero heroItemDataSource)
        {
            return new HeroResolverModel
            {
                HeroImages = heroItemDataSource.HeroImages,
                HeroTitle = heroItemDataSource.HeroTitle
            };
        }
    }
}
