using Helixbase.Foundation.Content.Models;

namespace Helixbase.Foundation.Redirects.Models
{
    public interface I301Redirect : ISitecoreItem
    {
        string RequestedURL { get; set; }
        ISitecoreItem RedirectItem { get; set; }
    }
}
