using System.Collections.Generic;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web;
using Sitecore.Data.Items;

namespace Helixbase.Foundation.Content.Repositories
{
    /// <summary>
    ///     Retrieve Sitecore items using Glass
    /// </summary>
    public class ContentRepository : IContentRepository
    {
        private readonly IRequestContext _requestContext;

        public ContentRepository(IRequestContext requestContext)
        {
            _requestContext = requestContext;
        }

        public T GetItem<T>(GetItemOptions options) where T : class
        {
           return _requestContext.SitecoreService.GetItem<T>(options);
        }

        public object GetItem(GetItemOptions options)
        {
            return _requestContext.SitecoreService.GetItem(options);
        }

        public IEnumerable<T> GetItems<T>(GetItemsOptions options) where T : class
        {
            return _requestContext.SitecoreService.GetItems<T>(options);
        }

        public object GetItems(GetItemsOptions options)
        {
            return _requestContext.SitecoreService.GetItems(options);
        }

        public T CreateItem<T>(CreateOptions options) where T : class
        {
            return _requestContext.SitecoreService.CreateItem<T>(options);
        }

        public object CreateItem(CreateOptions options)
        {
            return _requestContext.SitecoreService.CreateItem(options);
        }

        public void SaveItem(SaveOptions options)
        {
            _requestContext.SitecoreService.SaveItem(options);
        }

        public void MoveItem(MoveByModelOptions options)
        {
            _requestContext.SitecoreService.MoveItem(options);
        }

        public void DeleteItem(DeleteByModelOptions options)
        {
            _requestContext.SitecoreService.DeleteItem(options);
        }

        public Item ContextItem => _requestContext.ContextItem;
    }
}