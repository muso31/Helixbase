using Helixbase.Foundation.Model.Models;

namespace Helixbase.Foundation.Repository.Models
{
    public interface IContentRepository
    {
        T GetContentItem<T>(string contentGuid) where T : class, ISitecoreItem;

        T GetCurrentItem<T>() where T : class, ISitecoreItem;
    }
}
