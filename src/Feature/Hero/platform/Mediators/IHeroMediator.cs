using Helixbase.Feature.Hero.Platform.ViewModels;
using Helixbase.Foundation.Core.Platform.Models;

namespace Helixbase.Feature.Hero.Platform.Mediators
{
    public interface IHeroMediator
    {
        MediatorResponse<HeroViewModel> RequestHeroViewModel();
    }
}
