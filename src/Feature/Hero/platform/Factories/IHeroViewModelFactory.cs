using Helixbase.Feature.Hero.Models;
using Helixbase.Feature.Hero.ViewModels;

namespace Helixbase.Feature.Hero.Factories
{
    public interface IHeroViewModelFactory
    {
        HeroViewModel CreateHeroViewModel(IHero heroItemDataSource, bool isExperienceEditor);
    }
}
