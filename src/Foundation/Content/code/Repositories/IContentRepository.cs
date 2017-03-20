namespace Helixbase.Foundation.Content.Repositories
{
    public interface IContentRepository
    {
        T GetContentItem<T>(string contentGuid) where T : class;
        T GetCurrentItem<T>() where T : class;
        T GetHomeItem<T>() where T : class;
        T GetRootItem<T>() where T : class;
        T QuerySingleRelative<T>(string query) where T : class;
    }
}
