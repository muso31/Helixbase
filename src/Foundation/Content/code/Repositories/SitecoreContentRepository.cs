using Glass.Mapper.Sc;

namespace Helixbase.Foundation.Content.Repositories
{
    /// <summary>
    /// Retrieve Sitecore items using Content API
    /// </summary>
    public class SitecoreContentRepository : IContentRepository
    {
        private readonly ISitecoreContext _sitecoreContext;

        public SitecoreContentRepository(ISitecoreContext sitecoreContext)
        {
            _sitecoreContext = sitecoreContext;
        }
        public T GetContentItem<T>(string contentItem, bool isLazy = false, bool inferType = false) where T : class
        {
            return _sitecoreContext.GetItem<T>(contentItem, isLazy, inferType);
        }
        public T GetCurrentItem<T>(bool isLazy = false, bool inferType = false) where T : class
        {
            return _sitecoreContext.GetCurrentItem<T>(isLazy, inferType);
        }
        public T GetHomeItem<T>(bool isLazy = false, bool inferType = false) where T : class
        {
            return _sitecoreContext.GetHomeItem<T>(isLazy, inferType);
        }
        public T GetRootItem<T>(bool isLazy = false, bool inferType = false) where T : class
        {
            return _sitecoreContext.GetRootItem<T>(isLazy, inferType);
        }
        public T QuerySingleRelative<T>(string query, bool isLazy = false, bool inferType = false) where T : class
        {
            return _sitecoreContext.QuerySingleRelative<T>(query, isLazy, inferType);
        }
    }
}