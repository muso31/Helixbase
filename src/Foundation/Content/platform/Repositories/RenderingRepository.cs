using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.Data.Items;

namespace Helixbase.Foundation.Content.Platform.Repositories
{
    /// <summary>
    ///     Retrieve Rendering item data using Glass
    /// </summary>
    public class RenderingRepository : IRenderingRepository
    {
        private readonly IMvcContext _mvcContext;

        public RenderingRepository(IMvcContext mvcContext)
        {
            _mvcContext = mvcContext;
        }

        public T GetDataSourceItem<T>(GetKnownOptions options) where T : class
        {
            return _mvcContext.GetDataSourceItem<T>(options);
        }

        public T GetDataSourceItem<T>() where T : class
        {
            return _mvcContext.GetDataSourceItem<T>();
        }

        public T GetPageContextItem<T>(GetKnownOptions options) where T : class
        {
            return _mvcContext.GetPageContextItem<T>(options);
        }

        public T GetPageContextItem<T>() where T : class
        {
            return _mvcContext.GetPageContextItem<T>();
        }

        public T GetRenderingItem<T>(GetKnownOptions options) where T : class
        {
            return _mvcContext.GetRenderingItem<T>(options);
        }

        public T GetRenderingItem<T>() where T : class
        {
            return _mvcContext.GetRenderingItem<T>();
        }

        public T GetRenderingParameters<T>() where T : class
        {
            return _mvcContext.GetRenderingParameters<T>();
        }

        public bool HasDataSource => _mvcContext.HasDataSource;

        public Item DataSourceItem => _mvcContext.DataSourceItem;

        public string RenderingParameters => _mvcContext.RenderingParameters;
    }
}
