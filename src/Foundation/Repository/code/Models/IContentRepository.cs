using Helixbase.Foundation.Model.Sitecore;

namespace Helixbase.Foundation.Repository.Models
{
    public interface IContentRepository
    {
        T GetContentItem<T>(string contentGuid) where T : class, ISitecoreItem;
    }
}
