using Helixbase.Feature.Hero.ResolverModels;
using Sitecore.Data.Items;

namespace Helixbase.Feature.Hero.Services
{
    public interface IHeroBuilder
    {
        HeroResolverModel GetHeroModel(Item contextItem);
    }
}