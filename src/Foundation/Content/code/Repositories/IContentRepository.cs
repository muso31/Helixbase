using Helixbase.Foundation.Content.Models;

namespace Helixbase.Foundation.Content.Repositories
{
    public interface IContentRepository
    {
        T GetContentItem<T>(string contentGuid) where T : class, ISitecoreItem;

        T GetCurrentItem<T>() where T : class, ISitecoreItem;
    }
}
