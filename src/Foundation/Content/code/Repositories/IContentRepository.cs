using Glass.Mapper.Sc;
using Sitecore.Data.Items;

namespace Helixbase.Foundation.Content.Repositories
{
    public interface IContentRepository
    {
        T GetItem<T>(GetItemOptions options) where T : class;
        object GetItem(GetItemOptions options);
        T GetCurrentItem<T>() where T : class;
        object GetCurrentItem(GetKnownOptions options);
        T GetCurrentItem<T>(GetKnownOptions options) where T : class;
        T GetHomeItem<T>() where T : class;
        T GetHomeItem<T>(GetKnownOptions options) where T : class;
        T GetRootItem<T>() where T : class;
        T GetRootItem<T>(GetKnownOptions options) where T : class;
        Item ContextItem { get; }
    }
}