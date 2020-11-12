using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Maps;
using Helixbase.Foundation.ORM.Models;

namespace Helixbase.Foundation.ORM.Mappings
{
    public class GlassMappings : SitecoreGlassMap<IGlassBase>
    {
        public override void Configure()
        {
            Map(config =>
            {
                config.AutoMap();
                config.Info(f => f.BaseTemplateIds).InfoType(SitecoreInfoType.BaseTemplateIds);
            });
        }
    }
}
