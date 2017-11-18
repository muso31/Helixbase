using Sitecore;

namespace Helixbase.Foundation.Content.Repositories
{
    /// <summary>
    ///     Retrive Cms information, wrapper for Sitecore Api calls
    /// </summary>
    public class CmsInfoRepository : ICmsInfoRepository
    {
        public bool IsExperienceEditor => Context.PageMode.IsExperienceEditor;

        /// <summary>
        ///     Similar to ContentRepository GetRootItem method
        /// </summary>
        /// <returns></returns>
        public string GetSiteRoot()
        {
            return Context.Site.RootPath;
        }
    }
}