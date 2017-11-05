namespace Helixbase.Foundation.Content.Repositories
{
    public interface ICmsInfoRepository
    {
        bool IsExperienceEditor { get; }
        string GetSiteRoot();
    }
}
