using Sitecore.Data.Items;

namespace Helixbase.Feature.Redirects.Models
{
    public interface I301Redirect : IRedirectGlassBase
    {
        string RequestedUrl { get; set; }
        Item RedirectItem { get; set; }
    }
}