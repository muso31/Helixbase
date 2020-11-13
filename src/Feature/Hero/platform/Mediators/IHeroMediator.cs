using Helixbase.Feature.Hero.ResolverModels;
using Helixbase.Foundation.Core.Models;

namespace Helixbase.Feature.Hero.Mediators
{
    public interface IHeroMediator
    {
        MediatorResponse<HeroResolverModel> RequestHeroViewModel();
    }
}
