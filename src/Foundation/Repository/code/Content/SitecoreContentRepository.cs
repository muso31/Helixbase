using Glass.Mapper.Sc;
using Helixbase.Foundation.Model.Sitecore;
using System;

namespace Helixbase.Foundation.Repository.Content
{
    public abstract class SitecoreContentRepository : IContentRepository
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
    }
}