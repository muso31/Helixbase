namespace Helixbase.Foundation.Content.Repositories
{
    public interface IRenderingRepository
    {
        bool HasDataSource();
        string GetDataSource();
        string GetRenderingParameters();
    }
}
