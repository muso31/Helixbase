namespace Helixbase.Foundation.Content.Repositories
{
    /// <summary>
    /// Retrive Cms information, wrapper for Sitecore Api calls
    /// </summary>
    public class CmsInfoRepository : ICmsInfoRepository
    {
        public bool IsExperienceEditor => Sitecore.Context.PageMode.IsExperienceEditor;

        public string GetSiteRoot()
        {
            return Sitecore.Context.Site.RootPath;
        }
    }
}