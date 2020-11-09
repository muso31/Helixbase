using System.Collections.Generic;
using Glass.Mapper.Sc.Fields;

namespace Helixbase.Feature.Hero.Platform.Models
{
    public interface IHero : IHeroGlassBase
    {
        string HeroTitle { get; set; }
        IEnumerable<Image> HeroImages { get; set; }
    }
}
