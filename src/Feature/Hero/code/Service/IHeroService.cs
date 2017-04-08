using Helixbase.Feature.Hero.Models;
using Helixbase.Foundation.Search.Models;

namespace Helixbase.Feature.Hero.Service
{
    public interface IHeroService
    {
        IHero GetHeroImages();
        BaseSearchResultItem GetHeroImagesSearch();
    }
}
