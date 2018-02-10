using Glass.Mapper.Sc;
using Sitecore.Data.Items;

namespace Helixbase.Foundation.Content.Repositories
{
    public interface IContentRepository
    {
        Item ContextItem { get; }
        T GetContentItem<T>(GetByItemOptions options) where T : class;
        T GetCurrentItem<T>(GetByItemOptions options) where T : class;
        T GetHomeItem<T>(GetByItemOptions options) where T : class;
        T GetRootItem<T>(GetByItemOptions options) where T : class;
    }
}