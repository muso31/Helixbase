using Helixbase.Foundation.Content.Models;

namespace Helixbase.Foundation.Redirects.Models
{
    public interface IRedirectSettings : ISitecoreItem
    {
        IRedirectFolder RedirectFolder { get; set; }
    }
}
