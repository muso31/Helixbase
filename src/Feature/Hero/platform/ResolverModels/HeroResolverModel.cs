using System.Collections.Generic;
using Glass.Mapper.Sc.Fields;

namespace Helixbase.Feature.Hero.ResolverModels
{
    public class HeroResolverModel
    {
        public string HeroTitle { get; set; }
        public IEnumerable<Image> HeroImages { get; set; }
    }
}
