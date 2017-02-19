using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Maps;
using Helixbase.Foundation.Search.Models;

namespace Helixbase.Foundation.Search.ORM
{
    public class GlassMappings : SitecoreGlassMap<GlassSearchResultItem>
    {
        public override void Configure()
        {
            Map(config =>
            {
                config.AutoMap();
                config.Id(f => f.ItemId);
                config.Info(f => f.Language).InfoType(SitecoreInfoType.Language);
                config.Info(f=> f.Version).InfoType(SitecoreInfoType.Version);
            });
        }
    }
}