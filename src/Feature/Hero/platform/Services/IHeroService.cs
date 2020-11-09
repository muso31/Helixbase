using Helixbase.Feature.Hero.Platform.Models;
using Helixbase.Foundation.Search.Platform.Models;

namespace Helixbase.Feature.Hero.Platform.Services
{
    public interface IHeroService
    {
        IHero GetHeroItems();
        BaseSearchResultItem GetHeroImagesSearch();
        bool IsExperienceEditor { get; }
    }
}
