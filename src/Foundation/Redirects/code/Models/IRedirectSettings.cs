using Helixbase.Foundation.Models.BaseItem;

namespace Helixbase.Foundation.Redirects.Models
{
    public interface IRedirectSettings : ISitecoreItem
    {
        IRedirectFolder RedirectFolder { get; set; }
    }
}
