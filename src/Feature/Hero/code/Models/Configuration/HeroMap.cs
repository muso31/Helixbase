using Glass.Mapper.Sc.Maps;

namespace Helixbase.Feature.Hero.Models.Configuration
{
    public class HeroMap : SitecoreGlassMap<IHero>
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