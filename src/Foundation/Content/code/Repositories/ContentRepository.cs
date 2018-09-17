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

        public T GetCurrentItem<T>() where T : class
        {
            return _requestContext.GetContextItem<T>();
        }

        public object GetCurrentItem(GetKnownOptions options)
        {
            return _requestContext.GetContextItem(options);
        }

        public T GetCurrentItem<T>(GetKnownOptions options) where T : class
        {
            return _requestContext.GetContextItem<T>(options);
        }

        public T GetHomeItem<T>() where T : class
        {
            return _requestContext.GetHomeItem<T>();
        }

        public T GetHomeItem<T>(GetKnownOptions options) where T : class
        {
            return _requestContext.GetHomeItem<T>(options);
        }

        public T GetRootItem<T>() where T : class
        {
            return _requestContext.GetRootItem<T>();
        }

        public T GetRootItem<T>(GetKnownOptions options) where T : class
        {
            return _requestContext.GetRootItem<T>(options);
        }
        
        public T CreateItem<T>(CreateByModelOptions options) where T : class
        {
            return _requestContext.SitecoreService.CreateItem<T>(options);
        }
        
        public T CreateItem<T>(CreateByNameOptions options) where T : class
        {
            return _requestContext.SitecoreService.CreateItem<T>(options);
        }

        public void SaveItem<T>(T item) where T : class
        {
            _requestContext.SitecoreService.SaveItem(item);
        }

        public void SaveItem(SaveOptions options)
        {
            _requestContext.SitecoreService.SaveItem(options);
        }
        
        public void MoveItem(MoveByModelOptions options)
        {
            _requestContext.SitecoreService.MoveItem(options);
        }

        public void DeleteItem<T>(T item) where T : class
        {
            _requestContext.SitecoreService.DeleteItem(item);
        }

        public void DeleteItem(DeleteByModelOptions options)
        {
            _requestContext.SitecoreService.DeleteItem(options);
        }

        public Item ContextItem => _requestContext.ContextItem;
    }
}