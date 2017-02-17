using Glass.Mapper.Sc.Maps;
using Helixbase.Feature.Hero.Models;

namespace Helixbase.Feature.Hero.ORM
{
    public class GlassMappings : SitecoreGlassMap<IHero>
    {
        public override void Configure()
        {
            Map(x =>
            {
                x.AutoMap();
                x.TemplateId(Templates.Hero.TemplateId);
                x.Field(f => f.HeroImages).FieldName("Hero Images");
            });
        }
    }
}