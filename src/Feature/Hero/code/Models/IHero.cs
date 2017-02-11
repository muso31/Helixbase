using Helixbase.Foundation.Model.Models;

namespace Helixbase.Feature.Hero.Models
{
    public interface IHero : ISitecoreItem
    {
        string ImageName { get; set; }
    }
}
