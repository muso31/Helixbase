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
        public T GetContentItem<T>(string contentItem) where T : class
        {
            return _sitecoreContext.GetItem<T>(contentItem);
        }
        public T GetCurrentItem<T>() where T : class
        {
            return _sitecoreContext.GetCurrentItem<T>();
        }
        public T GetHomeItem<T>() where T : class
        {
            return _sitecoreContext.GetHomeItem<T>();
        }
        public T GetRootItem<T>() where T : class
        {
            return _sitecoreContext.GetRootItem<T>();
        }
        public T QuerySingleRelative<T>(string query) where T : class
        {
            return _sitecoreContext.QuerySingleRelative<T>(query);
        }
    }
}