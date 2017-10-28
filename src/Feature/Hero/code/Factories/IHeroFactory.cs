using Helixbase.Feature.Hero.ViewModels;
using Helixbase.Foundation.Search.Models;

namespace Helixbase.Feature.Hero.Factories
{
    public interface IHeroFactory
    {
        HeroViewModel CreateHeroViewModel();
        BaseSearchResultItem GetHeroImagesSearch();
    }
}
