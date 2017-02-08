using Helixbase.Foundation.Model.Sitecore;

namespace Helixbase.Feature.Hero.Models
{
    public interface IHero : ISitecoreItem
    {
        string ImageName { get; set; }
    }
}
