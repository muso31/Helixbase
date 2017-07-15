using Glass.Mapper.Sc.Maps;
using Helixbase.Feature.Hero.Models;

namespace Helixbase.Feature.Hero.ORM
{
    public class GlassMappings : SitecoreGlassMap<IHero>
    {
        public override void Configure()
        {
            Map(config =>
            {
                config.AutoMap();
                config.TemplateId(Templates.Hero.TemplateId);
                config.Self(f => f.ScItemBaseComp);
                config.Field(f => f.HeroImages).FieldName("Hero Images");
            });
        }
    }
}