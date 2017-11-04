using Helixbase.Feature.Hero.ViewModels;
using Helixbase.Foundation.Models.Mediators;

namespace Helixbase.Feature.Hero.Mediator
{
    public interface IHeroMediator
    {
        MediatorResponse<HeroViewModel> CreateHeroViewModel();
    }
}
