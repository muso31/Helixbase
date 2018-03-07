using Glass.Mapper.Sc.Web;

namespace Helixbase.Foundation.Content.Repositories
{
    /// <summary>
    ///     Retrieve Rendering data using Glass
    /// </summary>
    public class RenderingRepository : IRenderingRepository
    {
        private readonly IRenderingContext _renderingContext;

        public RenderingRepository(IRenderingContext renderingContext)
        {
            _renderingContext = renderingContext;
        }

        public bool HasDataSource()
        {
            return _renderingContext.HasDataSource;
        }

        public string GetDataSource()
        {
            return _renderingContext.GetDataSource();
        }

        public string GetRenderingParameters()
        {
            return _renderingContext.GetRenderingParameters();
        }
    }
}