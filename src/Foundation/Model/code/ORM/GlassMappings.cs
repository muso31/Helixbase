using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Maps;
using Helixbase.Foundation.Model.Models;

namespace Helixbase.Foundation.Model.ORM
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