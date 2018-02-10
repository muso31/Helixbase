using Glass.Mapper.Sc;
using Sitecore.Data.Items;

namespace Helixbase.Foundation.Content.Repositories
{
    public interface IRenderingRepository
    {
        T GetDataSource<T>(GetByItemOptions options) where T : class;

        T GetPageContextItem<T>(GetByItemOptions options) where T : class;

        T GetRenderingItem<T>(GetByItemOptions options) where T : class;

        bool HasDataSource { get; }

        Item GetDataSourceItem { get; }

        string GetRenderingParameters { get; }
    }
}
