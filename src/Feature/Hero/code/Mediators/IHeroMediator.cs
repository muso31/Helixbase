using Helixbase.Feature.Hero.ViewModels;
using Helixbase.Foundation.Core.Models;

namespace Helixbase.Feature.Hero.Mediators
{
    public interface IHeroMediator
    {
        MediatorResponse<HeroViewModel> CreateHeroViewModel();
    }
}
