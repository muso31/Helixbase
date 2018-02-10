using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.Data.Items;

namespace Helixbase.Foundation.Content.Repositories
{
    /// <summary>
    ///     Retrieve Rendering item data using Glass
    /// </summary>
    public class RenderingRepository : IRenderingRepository
    {
        private readonly IMvcContext _mvcContext;

        public T GetDataSource<T>(GetByItemOptions options) where T : class
        {
            return _mvcContext.GetDataSource<T>(options);
        }

        public T GetPageContextItem<T>(GetByItemOptions options) where T : class
        {
            return _mvcContext.GetPageContextItem<T>(options);
        }

        public T GetRenderingItem<T>(GetByItemOptions options) where T : class
        {
            return _mvcContext.GetRenderingItem<T>(options);
        }

        public bool HasDataSource => _mvcContext.HasDataSource;

        public Item GetDataSourceItem => _mvcContext.DataSourceItem;

        public string GetRenderingParameters => _mvcContext.RenderingParameters;
    }
}