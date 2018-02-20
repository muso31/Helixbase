using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web;
using Sitecore.Data.Items;

namespace Helixbase.Foundation.Content.Repositories
{
    /// <summary>
    /// Retrieve Sitecore items using Glass
    /// </summary>
    public class ContentRepository : IContentRepository
    {
        private readonly IRequestContext _requestContext;

        public ContentRepository(IRequestContext requestContext)
        {
            _requestContext = requestContext;
        }

        public T GetCurrentItem<T>(GetByItemOptions options) where T : class
        {
            return _requestContext.GetContextItem<T>(options);
        }

        public T GetHomeItem<T>(GetByItemOptions options) where T : class
        {
            return _requestContext.GetHomeItem<T>(options);
        }

        public T GetRootItem<T>(GetByItemOptions options) where T : class
        {
            return _requestContext.GetRootItem<T>(options);
        }

        public Item ContextItem => _requestContext.ContextItem;
    }
}