using Glass.Mapper.Sc.Maps;
using Helixbase.Feature.Redirects.Models;

namespace Helixbase.Feature.Redirects.ORM
{
    public class RedirectFolderMap : SitecoreGlassMap<IRedirectFolder>
    {
        public override void Configure()
        {
            Map(config =>
            {
                config.AutoMap();
                config.TemplateId(Templates.RedirectFolder.TemplateId);
                config.Self(f => f.ScItemBaseComp);
                config.Query(y => y.Children)
                    .Query($".//*[@@templateid='{Templates.RedirectContentItem.TemplateId.ToString("B").ToUpper()}']")
                    .IsRelative();
            });
        }
    }

    public class RedirectMap : SitecoreGlassMap<I301Redirect>
    {
        public override void Configure()
        {
            Map(config =>
            {
                config.AutoMap();
                config.TemplateId(Templates.RedirectDataItem.TemplateId);
                config.Self(f => f.ScItemBaseComp);
            });
        }
    }
}