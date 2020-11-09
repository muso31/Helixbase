using Helixbase.Feature.Hero.Platform.Models;
using Helixbase.Feature.Hero.Platform.ViewModels;

namespace Helixbase.Feature.Hero.Platform.Factories
{
    public interface IHeroViewModelFactory
    {
        HeroViewModel CreateHeroViewModel(IHero heroItemDataSource, bool isExperienceEditor);
    }
}
