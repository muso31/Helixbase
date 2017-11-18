using Helixbase.Foundation.Models.BaseItem;

namespace Helixbase.Feature.Redirects.Models
{
    public interface I301Redirect : ISitecoreItem
    {
        string RequestedUrl { get; set; }
        ISitecoreItem RedirectItem { get; set; }
    }
}