using Glass.Mapper.Sc;
using Sitecore.ContentSearch;
using Sitecore.Data.Items;

namespace Helixbase.Foundation.Content.Repositories
{
    public interface IContextRepository
    {
        bool IsExperienceEditor { get; }
        string GetContextSiteRoot();
        string GetContextStartItem();
        string GetDatabaseContext();
        T GetCurrentItem<T>() where T : class;
        object GetCurrentItem(GetKnownOptions options);
        T GetCurrentItem<T>(GetKnownOptions options) where T : class;
        T GetHomeItem<T>() where T : class;
        T GetHomeItem<T>(GetKnownOptions options) where T : class;
        T GetRootItem<T>() where T : class;
        T GetRootItem<T>(GetKnownOptions options) where T : class;

        ISearchIndex GetSearchIndexContext(Item contextItem);
    }
}
