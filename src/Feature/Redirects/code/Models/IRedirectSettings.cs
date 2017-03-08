using Helixbase.Foundation.Content.Models;

namespace Helixbase.Feature.Redirects.Models
{
    public interface IRedirectSettings : ISitecoreItem
    {
        IRedirectFolder RedirectFolder { get; set; }
    }
}
