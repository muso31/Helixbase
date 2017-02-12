using Glass.Mapper.Sc;
using Helixbase.Foundation.Model.Models;
using System;

namespace Helixbase.Foundation.Repository.Models
{
    public class SitecoreContentRepository : IContentRepository
    {
        private readonly ISitecoreContext _sitecoreContext;

        public SitecoreContentRepository(ISitecoreContext sitecoreContext)
        {
            _sitecoreContext = sitecoreContext;
        }
        public T GetContentItem<T>(string contentGuid) where T : class, ISitecoreItem
        {
            return _sitecoreContext.GetItem<T>(Guid.Parse(contentGuid));
        }
        public T GetCurrentItem<T>() where T : class, ISitecoreItem
        {
            return _sitecoreContext.GetCurrentItem<T>();
        }
    }
}