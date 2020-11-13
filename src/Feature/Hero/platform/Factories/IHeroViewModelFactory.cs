using Helixbase.Feature.Hero.Models;
using Helixbase.Feature.Hero.ResolverModels;

namespace Helixbase.Feature.Hero.Factories
{
    public interface IHeroViewModelFactory
    {
        HeroResolverModel CreateHeroViewModel(IHero heroItemDataSource);
    }
}
