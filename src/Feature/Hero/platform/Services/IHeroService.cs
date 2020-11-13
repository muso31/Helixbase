using Helixbase.Feature.Hero.Models;
using Helixbase.Foundation.Search.Models;

namespace Helixbase.Feature.Hero.Services
{
    public interface IHeroService
    {
        IHero GetHeroItems();
        BaseSearchResultItem GetHeroImagesSearch(); }
}
