namespace Helixbase.Foundation.Content.Repositories
{
    public interface IContentRepository
    {
        T GetContentItem<T>(string contentItem, bool isLazy = false, bool inferType = false) where T : class;
        T GetCurrentItem<T>(bool isLazy = false, bool inferType = false) where T : class;
        T GetHomeItem<T>(bool isLazy = false, bool inferType = false) where T : class;
        T GetRootItem<T>(bool isLazy = false, bool inferType = false) where T : class;
        T QuerySingle<T>(string query, bool isLazy = false, bool inferType = false) where T : class;
        T QuerySingleRelative<T>(string query, bool isLazy = false, bool inferType = false) where T : class;
        bool HasDataSource();
        string GetDataSource();
        string GetRenderingParameters();
    }
}
