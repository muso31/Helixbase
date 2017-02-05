using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Maps;

namespace Helixbase.Foundation.Model.Sitecore.Configuration
{
    public class SitecoreItemMap : SitecoreGlassMap<ISitecoreItem>
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