using Glass.Mapper.Sc;
using Helixbase.Foundation.Content.Models;
using System;

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
    }
}