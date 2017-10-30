using Glass.Mapper.Sc.Fields;
using Helixbase.Foundation.Models.BaseItem;
using System.Collections.Generic;

namespace Helixbase.Feature.Hero.Models
{
    public interface IHero : ISitecoreItem
    {
        string HeroTitle { get; set; }
        IEnumerable<Image> HeroImages { get; set; }
    }
}
