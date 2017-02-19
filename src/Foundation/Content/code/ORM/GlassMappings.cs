using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Maps;
using Helixbase.Foundation.Content.Models;

namespace Helixbase.Foundation.Content.ORM
{
    public class GlassMappings : SitecoreGlassMap<ISitecoreItem>
    {
        public override void Configure()
        {
            Map(x =>
            {
                x.AutoMap();
                x.Info(y => y.BaseTemplateIds).InfoType(SitecoreInfoType.BaseTemplateIds);
            });
        }
    }
}