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
        /// <summary>
        /// Gets an item using path or guid.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contentItem">Guid or path to item.</param>
        /// <param name="isLazy">if set to <c>true</c> [is lazy].</param>
        /// <param name="inferType">if set to <c>true</c> [infer type].</param>
        /// <returns>``0.</returns>
        public T GetContentItem<T>(string contentItem, bool isLazy = false, bool inferType = false) where T : class
        {
            return _sitecoreContext.GetItem<T>(contentItem, isLazy, inferType);
        }
        
        /// <summary>
        /// Gets current item as specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isLazy">if set to <c>true</c> [is lazy].</param>
        /// <param name="inferType">if set to <c>true</c> [infer type].</param>
        public T GetCurrentItem<T>(bool isLazy = false, bool inferType = false) where T : class
        {
            return _sitecoreContext.GetCurrentItem<T>(isLazy, inferType);
        }
        
        /// <summary>
        /// Get home item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isLazy">if set to <c>true</c> [is lazy].</param>
        /// <param name="inferType">if set to <c>true</c> [infer type].</param>
        public T GetHomeItem<T>(bool isLazy = false, bool inferType = false) where T : class
        {
            return _sitecoreContext.GetHomeItem<T>(isLazy, inferType);
        }

        /// <summary>
        /// Get root item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isLazy">if set to <c>true</c> [is lazy].</param>
        /// <param name="inferType">if set to <c>true</c> [infer type].</param>
        public T GetRootItem<T>(bool isLazy = false, bool inferType = false) where T : class
        {
            return _sitecoreContext.GetRootItem<T>(isLazy, inferType);
        }

        /// <summary>
        /// Query relative to the current item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="isLazy">if set to <c>true</c> [is lazy].</param>
        /// <param name="inferType">if set to <c>true</c> [infer type].</param>
        public T QuerySingleRelative<T>(string query, bool isLazy = false, bool inferType = false) where T : class
        {
            return _sitecoreContext.QuerySingleRelative<T>(query, isLazy, inferType);
        }
    }
}