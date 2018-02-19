using Sitecore;
using Sitecore.ContentSearch;
using Sitecore.Data.Items;

namespace Helixbase.Foundation.Content.Repositories
{
    /// <summary>
    ///     Retrive Cms information, wrapper for Sitecore Api calls
    /// </summary>
    public class ContextRepository : IContextRepository
    {
        public bool IsExperienceEditor => Context.PageMode.IsExperienceEditor;

        /// <summary>
        ///     Similar to ContentRepository GetRootItem method
        /// </summary>
        /// <returns></returns>
        public string GetContextSiteRoot() => Context.Site.RootPath;

        public string GetContextStartItem() => Context.Site.StartItem;

        public string GetDatabaseContext() => Context.Database.Name;

        public ISearchIndex GetSearchIndexContext(Item contextItem) => ContentSearchManager.GetIndex(new SitecoreIndexableItem(contextItem));
    }
}