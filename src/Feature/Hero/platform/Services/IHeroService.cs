using Helixbase.Feature.Hero.Models;
using Helixbase.Foundation.Search.Models;
using Sitecore.Data.Items;

namespace Helixbase.Feature.Hero.Services
{
    public interface IHeroService
    {
        IHero GetHeroItems(Item contextItem);
        BaseSearchResultItem GetHeroImagesSearch(); }
}
