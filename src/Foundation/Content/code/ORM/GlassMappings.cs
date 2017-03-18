using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Maps;
using Helixbase.Foundation.Content.Models;

namespace Helixbase.Foundation.Content.ORM
{
    public class SitecoreItemMap : SitecoreGlassMap<ISitecoreItem>
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
    public class SitecoreRootItemMap : SitecoreGlassMap<ISiteRoot>
    {
        public override void Configure()
        {
            Map(config =>
            {
                config.AutoMap();
                config.TemplateId(Templates.SiteRoot.TemplateId);
            });
        }
    }
}