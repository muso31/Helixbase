using Helixbase.Foundation.ORM.Models;

namespace Helixbase.Feature.Redirects.Models
{
    public interface I301Redirect : IRedirectGlassBase
    {
        string RequestedUrl { get; set; }
        IGlassBase RedirectItem { get; set; }
    }
}