using Glass.Mapper.Sc;
using Sitecore.Data.Items;

namespace Helixbase.Foundation.Content.Repositories
{
    public interface IRenderingRepository
    {
        T GetDataSourceItem<T>(GetKnownOptions options) where T : class;
        T GetDataSourceItem<T>() where T : class;
        T GetPageContextItem<T>(GetKnownOptions options) where T : class;
        T GetPageContextItem<T>() where T : class;
        T GetRenderingItem<T>(GetKnownOptions options) where T : class;
        T GetRenderingItem<T>() where T : class;
        T GetRenderingParameters<T>() where T : class;

        bool HasDataSource { get; }
        Item DataSourceItem { get; }
        string RenderingParameters { get; }
    }
}
