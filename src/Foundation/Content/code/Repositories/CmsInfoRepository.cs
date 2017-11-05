namespace Helixbase.Foundation.Content.Repositories
{
    public class CmsInfoRepository : ICmsInfoRepository
    {
        public bool IsExperienceEditor => Sitecore.Context.PageMode.IsExperienceEditor;

        public string GetSiteRoot()
        {
            return Sitecore.Context.Site.RootPath;
        }
    }
}