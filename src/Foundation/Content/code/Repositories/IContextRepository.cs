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
        ISearchIndex GetSearchIndexContext(Item contextItem);
    }
}
